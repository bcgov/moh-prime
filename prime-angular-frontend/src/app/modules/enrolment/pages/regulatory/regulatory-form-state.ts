import { FormArray, FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { ConfigService } from '@config/config.service';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';

export interface RegulatoryFormModel {
  certifications: CollegeCertification[];
  deviceProviderIdentifier?: string;
}

export class RegulatoryFormState extends AbstractFormState<RegulatoryFormModel> {
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

  public get deviceProviderIdentifier(): FormControl {
    return this.formInstance.get('deviceProviderIdentifier') as FormControl;
  }

  /**
   * @description
   * Access to college certifications where a self-documenting
   * API is beneficial for readability.
   *
   * @alias json
   */
  public get collegeCertifications(): CollegeCertification[] {
    const { certifications } = this.json;
    return certifications;
  }

  public get json(): RegulatoryFormModel {
    if (!this.formInstance) {
      return;
    }

    const certifications = this.certifications.getRawValue().map(c => {
      const { nurseCategory, ...collegeCertification } = c;
      return collegeCertification;
    });

    const deviceProviderIdentifier = this.deviceProviderIdentifier.value;

    return { certifications, deviceProviderIdentifier }
  }

  public patchValue(regulatoryFormModel: RegulatoryFormModel): void {
    const { certifications, deviceProviderIdentifier } = regulatoryFormModel;

    if (!this.formInstance || !Array.isArray(certifications)) {
      return;
    }

    this.removeCollegeCertifications();

    if (certifications.length) {
      certifications.forEach((c: CollegeCertification) => this.addCollegeCertification(c));
    }

    this.certifications.patchValue(certifications);
    this.deviceProviderIdentifier.patchValue(deviceProviderIdentifier);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      certifications: this.fb.array([]),
      deviceProviderIdentifier: [null, []]
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
      practitionerId: [null, []],
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
