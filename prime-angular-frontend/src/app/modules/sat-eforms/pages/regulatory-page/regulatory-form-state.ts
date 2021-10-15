import { FormArray, FormBuilder, FormGroup } from '@angular/forms';

import { ConfigService } from '@config/config.service';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { RegulatoryForm } from './regulatory-form.model';

export class RegulatoryFormState extends AbstractFormState<RegulatoryForm> {
  public constructor(
    private fb: FormBuilder,
    private configService: ConfigService,
    private certificationsKey: string
  ) {
    super();
    this.buildForm();
  }

  public get certifications(): FormArray {
    return this.formInstance.get(this.certificationsKey) as FormArray;
  }

  public get selectedCollegeCodes(): number[] {
    return this.certifications.value
      .map((certification: CollegeCertification) => +certification.collegeCode);
  }

  /**
   * @description
   * Access to college certifications where a self-documenting
   * API is beneficial for readability.
   *
   * @alias json
   */
  public get collegeCertifications(): RegulatoryForm {
    return this.json;
  }

  public get json(): RegulatoryForm {
    if (!this.formInstance) {
      return;
    }

    return this.formInstance.getRawValue();
  }

  public patchValue(model: RegulatoryForm): void {
    const certifications = model[this.certificationsKey];
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

  public buildForm(): void {
    this.formInstance = this.fb.group({
      [this.certificationsKey]: this.fb.array([]),
    });
  }

  public buildCollegeCertificationForm(): FormGroup {
    return this.fb.group({
      // Force selection of "None" on new certifications
      collegeCode: ['', []],
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

  public removeCollegeCertifications() {
    this.certifications.clear();
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
}
