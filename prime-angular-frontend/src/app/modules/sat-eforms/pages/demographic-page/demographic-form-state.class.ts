import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { Address, AddressType, addressTypes } from '@lib/models/address.model';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';

import { DemographicForm } from '@sat/pages/demographic-page/demographic-form.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';
import { ConfigService } from '@config/config.service';

export class DemographicFormState extends AbstractFormState<DemographicForm> {
  public constructor(
    private fb: FormBuilder,
    private configService: ConfigService,
    private formUtilsService: FormUtilsService,
    private certificationsKey: string
  ) {
    super();

    this.buildForm();
  }

  public get phone(): FormControl {
    return this.formInstance.get('phone') as FormControl;
  }

  public get email(): FormControl {
    return this.formInstance.get('email') as FormControl;
  }

  public get certifications(): FormArray {
    return this.formInstance.get(this.certificationsKey) as FormArray;
  }

  public get selectedCollegeCodes(): number[] {
    return this.certifications.value
      .map((certification: CollegeCertification) => +certification.collegeCode);
  }

  public get json(): DemographicForm {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: DemographicForm): void {
    if (!this.formInstance) {
      return;
    }

    this.jsonToForm(this.formInstance, model);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      email: [null, [
        Validators.required,
        FormControlValidators.email
      ]],
      phone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      [this.certificationsKey]: this.fb.array([])
    });
  }

  public buildCollegeCertificationForm(): FormGroup {
    return this.fb.group({
      // Force selection of "None" on new certifications
      collegeCode: ['', [Validators.required]],
      nurseCategory: [null, []],
      licenseCode: [null, []],
      // Validators are applied at the component-level when
      // fields are made visible to allow empty submissions
      licenseNumber: [null, []],
      renewalDate: [null, []],
      practiceCode: [null, []],
      practitionerId: [null, []]
    });
  }

  public addCollegeCertification(collegeCertification?: CollegeCertification) {
    const certification = this.buildCollegeCertificationForm();

    if (collegeCertification) {
      // Nursing category is a derived field for BCCNM, which is used to filter the
      // results for the verbose number of available licence codes for nurses
      const nurseCategory = (collegeCertification.collegeCode === CollegeLicenceClassEnum.BCCNM)
        ? this.configService.colleges
          .find(c => c.code === CollegeLicenceClassEnum.BCCNM)
          .collegeLicenses
          .filter(cl => cl.collegeCode === collegeCertification.collegeCode && cl.licenseCode === collegeCertification.licenseCode)
          .shift()
          .collegeLicenseGroupingCode
        : null;

      certification.patchValue({ ...collegeCertification, nurseCategory });
    }

    this.certifications.push(certification);
  }

  public addEmptyCollegeCertification() {
    this.addCollegeCertification();
  }

  /**
  * @description
  * Removes a certification from the list in response to an
  * emitted event from college certifications. Does not allow
  * the list of certifications to empty.
  *
  * @param index to be removed
  */
  public removeCertification(index: number) {
    this.certifications.removeAt(index);
  }

  public removeCollegeCertifications() {
    this.certifications.clear();
  }

  /**
   * @description
   * Removes incomplete certifications from the list in preparation
   * for submission, and allows for an empty list of certifications.
   */
  public removeIncompleteCertifications(noEmptyCert: boolean = false) {
    this.certifications.controls
      .forEach((control: FormGroup, index: number) => {
        // Remove if college code is "None" or the group is invalid
        if (!control.get('collegeCode').value || control.invalid) {
          this.removeCertification(index);
        }
      });

    // Always have a single certification available, and it prevents
    // the page from jumping too much when routing
    if (!noEmptyCert && !this.certifications.controls.length) {
      this.addEmptyCollegeCertification();
    }
  }

  /**
   * @description
   * Sanitize JSON for patching the reactive form.
   */
  private jsonToForm(formGroup: FormGroup, data: DemographicForm): void {
    if (data) {
      formGroup.patchValue(data);

      const certifications = data[this.certificationsKey];
      if (!this.formInstance || !Array.isArray(certifications) || !certifications.length) {
        return;
      }

      if (certifications.length) {
        this.removeCollegeCertifications();
        certifications.forEach((c: CollegeCertification) =>
          this.addCollegeCertification(c)
        );
      }

      this.certifications.patchValue(certifications);
    }
  }
}
