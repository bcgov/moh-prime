import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { ConfigService } from '@config/config.service';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';

export interface RegulatoryFormModel {
  certifications: CollegeCertification[];
}

// TODO use RegulatoryFormModel instead of CollegeCertification[], which
// makes the passing in and out of form state more like using specific
// keys from the original model
export class RegulatoryFormState extends AbstractFormState<CollegeCertification[]> {
  public constructor(
    private fb: FormBuilder,
    private configService: ConfigService
  ) {
    super();
    this.buildForm();
  }

  public get certifications(): FormArray {
    return this.formInstance.get('certifications') as FormArray;
  }

  /**
   * @description
   * Access to college certifications where a self-documenting
   * API is beneficial for readability.
   *
   * @alias json
   */
  public get collegeCertifications(): CollegeCertification[] {
    return this.json;
  }

  public get json(): CollegeCertification[] {
    if (!this.formInstance) {
      return;
    }

    return this.certifications.getRawValue().map(c => {
      const { nurseCategory, ...collegeCertification } = c;
      return collegeCertification;
    });
  }

  public patchValue(certifications: CollegeCertification[]): void {
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
      certifications: this.fb.array([]),
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

  public removeCollegeCertifications() {
    this.certifications.clear();
  }
}
