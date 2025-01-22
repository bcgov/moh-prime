import { FormArray, FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

import { CollegeConfig } from '@config/config.model';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { ConfigService } from '@config/config.service';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';

import { EnrolmentRegulatoryForm } from './enrolment-regulatory-form.model';
import { UnlistedCertification } from '@paper-enrolment/shared/models/unlisted-certification.model';
export class RegulatoryFormState extends AbstractFormState<EnrolmentRegulatoryForm> {
  public colleges: CollegeConfig[];

  public constructor(
    protected fb: FormBuilder,
    protected configService: ConfigService
  ) {
    super();
    this.buildForm();
    this.colleges = this.configService.colleges;
  }

  public get certifications(): FormArray {
    return this.formInstance.get('certifications') as FormArray;
  }

  public get deviceProviderIdentifier(): FormControl {
    return this.formInstance.get('deviceProviderIdentifier') as FormControl;
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

  public get unlistedCertifications(): FormArray {
    return this.formInstance.get('unlistedCertifications') as FormArray;
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
      const { category, ...collegeCertification } = c;
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

    if (!this.formInstance || !Array.isArray(certifications)) {
      return;
    }

    //clear the form array before patching the value
    this.removeCollegeCertifications();
    this.removeUnlistedCertifications();

    if (certifications.length) {
      certifications.forEach((c: CollegeCertification) => this.addCollegeCertification(c));
    }
    if (unlistedCertifications && unlistedCertifications.length) {
      unlistedCertifications.forEach((c: UnlistedCertification) => this.addUnlistedCertification(c));
    }
    if (enrolleeDeviceProviders && enrolleeDeviceProviders.length) {
      const { deviceProviderId, deviceProviderRoleCode, certificationNumber } = enrolleeDeviceProviders[0];
      this.formInstance.patchValue({ certifications, deviceProviderId, deviceProviderRoleCode, certificationNumber });
    } else {
      this.formInstance.patchValue({ certifications });
    }
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      certifications: this.fb.array([]),
      deviceProviderIdentifier: [null, []],
      deviceProviderId: [null, []],
      deviceProviderRoleCode: [null, []],
      certificationNumber: [null, []],
      unlistedCertifications: this.fb.array([]),
    });
  }

  public buildCollegeCertificationForm(): FormGroup {
    return this.fb.group({
      // Force selection of "None" on new certifications
      collegeCode: ['', []],
      category: [null, []],
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
      const category = this.collegeHasGrouping(collegeCertification.collegeCode)
        ? this.configService.colleges
          .find(c => c.code === collegeCertification.collegeCode)
          .collegeLicenses
          .filter(cl => cl.collegeCode === collegeCertification.collegeCode && cl.licenseCode === collegeCertification.licenseCode)
          .shift()
          .collegeLicenseGroupingCode
        : null;

      certification.patchValue({ ...collegeCertification, category });
    }

    this.certifications.push(certification);
  }

  public removeCollegeCertifications() {
    this.certifications.clear();
  }

  public removeUnlistedCertifications() {
    this.unlistedCertifications.clear();
  }

  public collegeHasGrouping(collegeCode: number): boolean {
    if (collegeCode === 0) {
      return false;
    }
    const college = this.colleges.find((c) => c.code === collegeCode);
    return college ? college.collegeLicenses.some((l) => l.collegeLicenseGroupingCode) : false;
  }

  public buildUnlistedCollegeCertificationForm(): FormGroup {
    return this.fb.group({
      collegeName: ['', []],
      licenceNumber: ['', []],
      licenceClass: ['', []],
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
}
