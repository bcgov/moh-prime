import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

import moment from 'moment';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { Config, CollegeConfig, LicenseConfig, PracticeConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ViewportService } from '@core/services/viewport.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';
import { NursingLicenseCode } from '@shared/enums/nursing-license-code.enum';
import { PrescriberIdTypeEnum } from '@shared/enums/prescriber-id-type.enum';
import { Observable } from 'tinymce';

@Component({
  selector: 'app-college-certification-form',
  templateUrl: './college-certification-form.component.html',
  styleUrls: ['./college-certification-form.component.scss']
})
export class CollegeCertificationFormComponent implements OnInit {
  @Input() public form: FormGroup;
  @Input() public index: number;
  @Input() public total: number;
  @Input() public selectedColleges: number[];
  @Input() public collegeFilterPredicate: (collegeConfig: CollegeConfig) => boolean;
  @Input() public licenceFilterPredicate: (licenceConfig: LicenseConfig) => boolean;
  @Input() public condensed: boolean;
  @Output() public remove: EventEmitter<number>;
  public isPrescribing: boolean;
  public colleges: CollegeConfig[];
  public licenses: LicenseConfig[];


  public subscription$: Observable<any>;
  /**
   * @description
   * Indicates the licenceCode is validated by PharmaNet.
   */
  public practices: PracticeConfig[];
  public filteredLicenses: Config<number>[];
  public filteredPractices: Config<number>[];
  public hasPractices: boolean;
  public prescriberIdType: PrescriberIdTypeEnum;
  public minRenewalDate: moment.Moment;
  public CollegeLicenceClassEnum = CollegeLicenceClassEnum;
  public PrescriberIdTypeEnum = PrescriberIdTypeEnum;

  constructor(
    private configService: ConfigService,
    private viewportService: ViewportService,
    private formUtilsService: FormUtilsService
  ) {
    this.remove = new EventEmitter<number>();
    this.colleges = this.configService.colleges;
    this.licenses = this.configService.licenses;
    this.practices = this.configService.practices;
    this.minRenewalDate = moment();
    this.condensed = false;
  }

  public get isMobile() {
    return this.viewportService.isMobile;
  }

  public get collegeCode(): FormControl {
    return this.form.get('collegeCode') as FormControl;
  }

  public get licenseNumber(): FormControl {
    return this.form.get('licenseNumber') as FormControl;
  }

  public get licenseCode(): FormControl {
    return this.form.get('licenseCode') as FormControl;
  }

  public get practitionerId(): FormControl {
    return this.form.get('practitionerId') as FormControl;
  }

  public get renewalDate(): FormControl {
    return this.form.get('renewalDate') as FormControl;
  }

  public get practiceCode(): FormControl {
    return this.form.get('practiceCode') as FormControl;
  }

  public get filteredColleges(): CollegeConfig[] {
    return this.colleges.filter((college: CollegeConfig) =>
      // Allow the currently chosen value to persist
      this.collegeCode.value === college.code || !this.selectedColleges.includes(college.code)
    );
  }

  public allowedColleges(): CollegeConfig[] {
    return (this.collegeFilterPredicate)
      ? this.filteredColleges.filter(this.collegeFilterPredicate)
      : this.filteredColleges;
  }

  public allowedLicenses() {
    return (this.licenceFilterPredicate)
      ? this.filteredLicenses.filter(this.licenceFilterPredicate)
      : this.filteredLicenses;
  }

  public removeCertification() {
    this.remove.emit(this.index);
  }

  public shouldShowPractices(): boolean {
    // Only display Advanced Practices for certain nursing licences
    return ((+this.collegeCode.value === CollegeLicenceClassEnum.BCCNM) && ([
      NursingLicenseCode.NON_PRACTICING_REGISTERED_NURSE,
      NursingLicenseCode.PRACTICING_REGISTERED_NURSE,
      NursingLicenseCode.PROVISIONAL_REGISTERED_NURSE,
      NursingLicenseCode.TEMPORARY_REGISTERED_NURSE_EMERGENCY,
      NursingLicenseCode.TEMPORARY_REGISTERED_NURSE_SPECIAL_EVENT
    ].includes(this.licenseCode.value)));
  }

  public onPrescribing({ checked }: MatSlideToggleChange): void {
    this.isPrescribing = checked;

    (checked)
      ? this.setPractitionerId(this.licenseCode.value)
      : this.resetPractitionerId();
  }

  public ngOnInit() {
    if (this.condensed) {
      this.formUtilsService.setValidators(this.collegeCode, [Validators.required]);
    }
    this.setCollegeCertification(this.collegeCode.value);

    this.collegeCode.valueChanges
      .subscribe((collegeCode: number) => {
        this.resetCollegeCertification();
        this.setCollegeCertification(collegeCode);
      });

    if (!this.condensed) {
      this.licenseCode.valueChanges
        .subscribe((licenseCode: number) => {
          this.prescriberIdType = this.prescriberIdTypeByLicenceCode(this.licenseCode.value);
          if (this.prescriberIdType !== PrescriberIdTypeEnum.Mandatory) {
            this.resetPractitionerId();
          }
          this.setPractitionerId(licenseCode);
        });
    }

    this.prescriberIdType = this.prescriberIdTypeByLicenceCode(this.licenseCode.value);
    this.isPrescribing = this.prescriberIdType === PrescriberIdTypeEnum.Optional && !!this.practitionerId.value;
    this.setPractitionerId(this.licenseCode.value);
  }

  private setCollegeCertification(collegeCode: number): void {
    if (!collegeCode) {
      this.removeValidations();
      return;
    }

    // Initialize the validations when the college code is not
    // "None" to allow for submission when no college is selected
    this.setCollegeCertificationValidators();

    this.loadLicenses(collegeCode);
    if (this.filteredLicenses?.length === 1) {
      // Trigger licenceCode value changes to manage setting up
      // remaining parts of the form
      this.licenseCode.patchValue(this.filteredLicenses[0].code);
    }

    if (!this.condensed) {
      this.loadPractices(collegeCode);
    }
  }

  private setCollegeCertificationValidators() {
    this.formUtilsService.setValidators(this.licenseCode, [Validators.required]);
    this.formUtilsService.setValidators(this.licenseNumber, [
      Validators.required,
      FormControlValidators.alphanumeric
    ]);

    if (!this.condensed) {
      this.formUtilsService.setValidators(this.renewalDate, [Validators.required]);
    }
  }

  private resetCollegeCertification() {
    this.licenseCode.reset(null);
    this.licenseNumber.reset(null);

    if (!this.condensed) {
      this.renewalDate.reset(null);
      this.practiceCode.reset(null);
      this.resetPractitionerId();
    }
  }

  private setPractitionerId(licenseCode: number) {
    this.prescriberIdType = this.prescriberIdTypeByLicenceCode(licenseCode);
    if (this.prescriberIdType === PrescriberIdTypeEnum.Mandatory || this.isPrescribing) {
      this.formUtilsService.setValidators(this.practitionerId, [
        Validators.required,
        FormControlValidators.numeric,
        FormControlValidators.requiredLength(5)
      ]);
    }
  }

  private resetPractitionerId() {
    this.isPrescribing = false;
    this.formUtilsService.resetAndClearValidators(this.practitionerId);
  }

  private prescriberIdTypeByLicenceCode(licenceCode: number): PrescriberIdTypeEnum {
    const prescriberIdTypes = this.licenses
      .filter(licenseConfig => licenseConfig.code === licenceCode)
      .map(licenseConfig => licenseConfig.prescriberIdType);

    return (prescriberIdTypes.length)
      ? prescriberIdTypes[0]
      : PrescriberIdTypeEnum.NA;
  }

  private removeValidations() {
    this.formUtilsService.setValidators(this.licenseCode, []);
    this.formUtilsService.setValidators(this.licenseNumber, []);

    if (!this.condensed) {
      this.formUtilsService.setValidators(this.renewalDate, []);
      this.formUtilsService.setValidators(this.practitionerId, []);
    }
  }

  private loadLicenses(collegeCode: number) {
    this.filteredLicenses = this.filterLicenses(collegeCode);
    this.licenseCode.patchValue(this.licenseCode.value || null, { emitEvent: false });
  }

  private loadPractices(collegeCode: number) {
    this.filteredPractices = this.filterPractices(collegeCode);
    this.practiceCode.patchValue(this.practiceCode.value || null);
    this.hasPractices = (this.filteredPractices.length) ? true : false;
  }

  private filterLicenses(collegeCode: number): LicenseConfig[] {
    return this.licenses.filter(l => l.collegeLicenses.map(cl => cl.collegeCode).includes(collegeCode));
  }

  private filterPractices(collegeCode: number): PracticeConfig[] {
    return this.practices.filter(p => p.collegePractices.map(cl => cl.collegeCode).includes(collegeCode));
  }
}
