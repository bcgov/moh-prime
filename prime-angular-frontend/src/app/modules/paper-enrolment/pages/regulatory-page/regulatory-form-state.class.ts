import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ConfigService } from '@config/config.service';
import { RegulatoryFormState as BaseRegulatoryPageFormState } from '@enrolment/pages/regulatory/regulatory-form-state';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { RegulatoryForm } from './regulatory-form.model';
import { UnlistedCertification } from '@paper-enrolment/shared/models/unlisted-certification.model';

export class RegulatoryFormState extends BaseRegulatoryPageFormState {

  public constructor(
    protected fb: FormBuilder,
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

  // public unlistedCertifications: FormArray;

  // public constructor() {
  //   super();
  //   this.unlistedCertifications = this.fb.group({
  //     unlistedCollegeName: ['', []],
  //     unlistedCollegeLicence: ['', []],
  //     unlistedRenewalDate: ['', []]
  //   })
  // }

  public get unlistedCertifications(): FormArray {
    return this.formInstance.get('unlistedCertifications') as FormArray;
  }
//   public unlistedCertifications = new FormArray([
//     new FormControl(),
//     new FormControl()
//  ]);


  // public get unlistedCollegeName(): FormControl {
  //   return this.form.get('unlistedCollegeName') as FormControl;
  // }

  // public get unlistedCollegeLicence(): FormControl {
  //   return this.form.get('unlistedCollegeLicence') as FormControl;
  // }

  // public get unlistedRenewalDate(): FormControl {
  //   return this.form.get('unlistedRenewalDate') as FormControl;
  // }

  public get json(): RegulatoryForm {
    if (!this.formInstance) {
      return;
    }

    const {
      certifications: rawCertifications,
      deviceProviderIdentifier,
      unlistedCertifications
    } = this.formInstance.getRawValue();
    const certifications = rawCertifications.map(c => {
      const { nurseCategory, ...collegeCertification } = c;
      return collegeCertification;
    });

    return {
      certifications,
      deviceProviderIdentifier,
      unlistedCertifications
    }
  }


  public patchValue({ certifications, deviceProviderIdentifier, unlistedCertifications }: RegulatoryForm): void {

    if (!this.formInstance || !Array.isArray(certifications) || !Array.isArray(unlistedCertifications)) {
      return;
    }

    this.removeCollegeCertifications();

    if (certifications.length) {
      certifications.forEach((c: CollegeCertification) => this.addCollegeCertification(c));
    }

    if (unlistedCertifications.length) {
      unlistedCertifications.forEach((uc: UnlistedCertification) => this.addUnlistedCertification(uc));
    }

    this.formInstance.patchValue({ certifications, deviceProviderIdentifier, unlistedCertifications });
  }

  // public buildForm(): void {
  //   this.formInstance = this.fb.group({
  //     certifications: this.fb.array([]),
  //     deviceProviderIdentifier: [null, []],
  //     unlistedCollegeName: ['', []],
  //     unlistedCollegeLicence: ['', []],
  //     unlistedRenewalDate: [null, []]
  //   });
  // }

  public buildForm(): void {
    this.formInstance = this.fb.group({
      certifications: this.fb.array([]),
      deviceProviderIdentifier: [null, []],
      unlistedCertifications: this.fb.array([])
    });
  }

  public buildUnlistedCollegeCertificationForm(): FormGroup {
    return this.fb.group({
      unlistedCollegeName: ['', []],
      unlistedCollegeCode: ['', []],
      unlistedRenewalDate: ['', []]
    })
  }

  public addUnlistedCertification(unlistedCertification?: UnlistedCertification): void {
    const unlistedCert = this.buildUnlistedCollegeCertificationForm();
    this.unlistedCertifications.push(unlistedCert);
  }

  public addEmptyUnlistedCollegeCertification(): void {
    // this.addUnlistedCertification();
    this.buildUnlistedCollegeCertificationForm();
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

  /**
   * @description
   * Removes incomplete certifications from the list in preparation
   * for submission, and allows for an empty list of certifications.
   */
  public removeIncompleteCertifications(noEmptyCert: boolean = false): void {
    this.certifications.controls
      .forEach((control: FormGroup, index: number) => {
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

  // public blah() {
  //   this.unlistedCertifications = this.fb.group({
  //     unlistedCollegeName: ['', []],
  //     unlistedCollegeLicence: ['', []],
  //     unlistedRenewalDate: ['', []]
  //   })
  // }
}
