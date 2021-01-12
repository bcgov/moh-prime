import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

export interface RegulatoryFormModel {
  certifications: CollegeCertification[];
}

export class RegulatoryFormState extends AbstractFormState<CollegeCertification[]> {
  public constructor(
    private fb: FormBuilder
  ) {
    super();

    this.buildForm();
  }

  public get certifications(): FormArray {
    return this.formInstance.get('certifications') as FormArray;
  }

  /**
   * @alias json
   */
  public get collegeCertifications(): CollegeCertification[] {
    return this.json;
  }

  public get json(): CollegeCertification[] {
    if (!this.formInstance) {
      return;
    }

    return this.certifications.getRawValue();
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
      // Validators are applied at the component-level when
      // fields are made visible to allow empty submissions
      licenseNumber: [null, []],
      licenseCode: [null, []],
      renewalDate: [null, []],
      practiceCode: [null, []]
    });
  }

  public addCollegeCertification(collegeCertification?: CollegeCertification) {
    const certification = this.buildCollegeCertificationForm();

    if (collegeCertification) {
      certification.patchValue(collegeCertification);
    }

    this.certifications.push(certification);
  }

  public removeCollegeCertifications() {
    this.certifications.clear();
  }
}
