import { FormArray, FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { UnlistedCertification } from '@paper-enrolment/shared/models/unlisted-certification.model';

import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { ConfigService } from '@config/config.service';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';

import { EnrolmentRegulatoryForm } from './enrolment-regulatory-form.model';
export class RegulatoryFormState extends AbstractFormState<EnrolmentRegulatoryForm> {
  public constructor(
    protected fb: FormBuilder,
    protected configService: ConfigService
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

  public get unlistedCertifications(): FormArray {
    return this.formInstance.get('unlistedCertifications') as FormArray;
  }

  public get deviceProviderRoleCode(): FormControl {
    return this.formInstance.get('deviceProviderRoleCode') as FormControl;
  }

  public get deviceProviderId(): FormControl {
    return this.formInstance.get('deviceProviderId') as FormControl;
  }

  public get certificationNumber(): FormControl {
    return this.formInstance.get('certificationNumber') as FormControl;
  }

  /**
   * @description
   * Access to college certifications where a self-documenting
   * API is beneficial for readability.
   *
   * @alias json
   */
  public get collegeCertifications(): CollegeCertification[] {
    return this.json.certifications;
  }

  public get json(): EnrolmentRegulatoryForm {
    if (!this.formInstance) {
      return;
    }

    const { certifications: rawCertifications, deviceProviderId, deviceProviderRoleCode, certificationNumber, unlistedCertifications } = this.formInstance.getRawValue();
    let certifications = rawCertifications.map(c => {
      const { nurseCategory, ...collegeCertification } = c;
      return collegeCertification;
    });

    if (certifications && certifications.length === 1 && certifications[0].collegeCode === "") {
      //reset certifications
      certifications = [];
    }

    const enrolleeDeviceProviders = deviceProviderRoleCode ?
      [{ deviceProviderId, deviceProviderRoleCode, certificationNumber }] : [];

    return { certifications, enrolleeDeviceProviders, unlistedCertifications }
  }

  public patchValue({ certifications, enrolleeDeviceProviders, unlistedCertifications }: EnrolmentRegulatoryForm): void {

    if (!this.formInstance || !Array.isArray(certifications) || !Array.isArray(unlistedCertifications)) {
      return;
    }

    this.removeCollegeCertifications();
    this.removeUnlistedCertifications();

    if (certifications.length) {
      certifications.forEach((c: CollegeCertification) => this.addCollegeCertification(c));
    }
    if (unlistedCertifications.length) {
      unlistedCertifications.forEach((u: UnlistedCertification) => this.addUnlistedCertification(u));
    }
    
    if (enrolleeDeviceProviders && enrolleeDeviceProviders.length) {
      const { deviceProviderId, deviceProviderRoleCode, certificationNumber } = enrolleeDeviceProviders[0];
      this.formInstance.patchValue({ certifications, deviceProviderId, deviceProviderRoleCode, certificationNumber, unlistedCertifications });
    } else {
      this.formInstance.patchValue({ certifications, unlistedCertifications });
    }
    this.unlistedCertifications.patchValue(unlistedCertifications);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      certifications: this.fb.array([]),
      unlistedCertifications: this.fb.array([]),
      deviceProviderId: [null, []],
      deviceProviderRoleCode: [null, []],
      certificationNumber: [null, []]
    });
  }

  public buildUnlistedCollegeCertificationForm(): FormGroup {
    return this.fb.group({
      collegeName: ['', []],
      licenceNumber: ['', []],
      renewalDate: ['', []]
    })
  }

  public addUnlistedCertification(unlistedCertification?: UnlistedCertification): void {
    const unlistedCert = this.buildUnlistedCollegeCertificationForm();
    unlistedCert.patchValue({ ...unlistedCertification });
    this.unlistedCertifications.push(unlistedCert);
  }

  public addEmptyUnlistedCollegeCertification(): void {
    this.addUnlistedCertification();
  }

  public removeUnlistedCertification(index: number): void {
    this.unlistedCertifications.removeAt(index);
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

  public removeIncompleteUnlistedCertifications(): void {
    this.unlistedCertifications.controls
      .forEach((control: FormGroup, index: number) => {
        // Remove if college code is null or the group is invalid
        if (!control.get('licenceNumber').value || control.invalid) {
          this.removeUnlistedCertification(index);
        }
      });
  }

  public removeCollegeCertifications() {
    this.certifications.clear();
  }

  public removeUnlistedCertifications() {
    this.unlistedCertifications.clear();
  }

  
}
