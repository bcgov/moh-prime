import { Injectable } from '@angular/core';
import { UntypedFormArray, UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { ConfigService } from '@config/config.service';
import { RegulatoryFormState as BaseRegulatoryPageFormState } from '@enrolment/pages/regulatory/regulatory-form-state';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { UnlistedCertification } from '@paper-enrolment/shared/models/unlisted-certification.model';
import { RegulatoryForm } from './regulatory-form.model';

@Injectable({
  providedIn: 'root'
})
export class RegulatoryFormState extends BaseRegulatoryPageFormState {

  public constructor(
    protected fb: UntypedFormBuilder,
    protected configService: ConfigService
  ) {
    super(fb, configService);
    this.buildForm();
  }

  public get selectedCollegeCodes(): number[] {
    return this.certifications.value
      .map((certification: CollegeCertification) => +certification.collegeCode);
  }

  public addEmptyCollegeCertification(): void {
    this.addCollegeCertification();
  }

  public get unlistedCertifications(): UntypedFormArray {
    return this.formInstance.get('unlistedCertifications') as UntypedFormArray;
  }

  public get json(): RegulatoryForm {
    if (!this.formInstance) {
      return;
    }

    const {
      certifications: rawCertifications,
      deviceProviderRoleCode,
      deviceProviderId,
      certificationNumber,
      unlistedCertifications
    } = this.formInstance.getRawValue();

    const certifications = rawCertifications.map(c => {
      const { category, ...collegeCertification } = c;
      return collegeCertification;
    });

    const enrolleeDeviceProviders = deviceProviderRoleCode ? [{
      deviceProviderRoleCode,
      deviceProviderId,
      certificationNumber,
    }] : [];

    return {
      certifications,
      enrolleeDeviceProviders,
      unlistedCertifications
    }
  }

  public patchValue({ certifications, enrolleeDeviceProviders, unlistedCertifications }: RegulatoryForm): void {

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
    const { deviceProviderRoleCode,
      deviceProviderId,
      certificationNumber } = enrolleeDeviceProviders[0];
    this.formInstance.patchValue({
      certifications, deviceProviderRoleCode,
      deviceProviderId, certificationNumber, unlistedCertifications
    });
    this.unlistedCertifications.patchValue(unlistedCertifications);
  }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      certifications: this.fb.array([]),
      deviceProviderIdentifier: [null, []],
      unlistedCertifications: this.fb.array([])
    });
  }

  public buildUnlistedCollegeCertificationForm(): UntypedFormGroup {
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

  /**
   * @description
   * Removes a certification from the list in response to an
   * emitted event from college certifications. Does not allow
   * the list of certifications to empty.
   */
  public removeCertification(index: number): void {
    this.certifications.removeAt(index);
  }

  public removeUnlistedCertification(index: number): void {
    this.unlistedCertifications.removeAt(index);
  }

  /**
   * @description
   * Removes incomplete certifications from the list in preparation
   * for submission, and allows for an empty list of certifications.
   */
  public removeIncompleteCertifications(noEmptyCert: boolean = false): void {
    this.certifications.controls
      .forEach((control: UntypedFormGroup, index: number) => {
        // Remove if college code is "None" or the group is invalid
        if (!control.get('collegeCode').value || control.invalid) {
          this.removeCertification(index);
        }
      });

    // Always have a single cerfication available, and it prevents
    // the page from jumping too much when routing
    if (!noEmptyCert && !this.certifications.controls.length) {
      this.addEmptyCollegeCertification();
    }
  }

  public removeIncompleteUnlistedCertifications(): void {
    this.unlistedCertifications.controls
      .forEach((control: UntypedFormGroup, index: number) => {
        // Remove if college code is null or the group is invalid
        if (!control.get('licenceNumber').value || control.invalid) {
          this.removeUnlistedCertification(index);
        }
      });
  }

  public removeUnlistedCertifications() {
    this.unlistedCertifications.clear();
  }

}
