import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';

import { startWith } from 'rxjs/operators';

import moment from 'moment';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { Config, CollegeConfig, LicenseConfig, PracticeConfig, CollegeLicenseGroupingConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ViewportService } from '@core/services/viewport.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';
import { NursingLicenseCode } from '@shared/enums/nursing-license-code.enum';
import { PrescriberIdTypeEnum } from '@shared/enums/prescriber-id-type.enum';

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
  /**
   * @description
   * Indicates the licenceCode is validated by PharmaNet.
   */
  public practices: PracticeConfig[];
  public filteredLicenses: Config<number>[];
  public filteredPractices: Config<number>[];
  public nurseGroups: CollegeLicenseGroupingConfig[];
  public hasPractices: boolean;
  /**
   * @description
   * Indicates the prescriber ID type associated with a
   * chosen licence code.
   *
   * NOTE: Used to show the practitioner ID form element
   * when Mandatory.
   */
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
    this.nurseGroups = this.configService.collegeLicenseGroupings;
    this.minRenewalDate = moment();
    this.condensed = false;
  }

  public get isMobile() {
    return this.viewportService.isMobile;
  }

  public get collegeCode(): FormControl {
    return this.form.get('collegeCode') as FormControl;
  }

  public get nurseCategory(): FormControl {
    return this.form.get('nurseCategory') as FormControl;
  }

  public get licenseNumber(): FormControl {
    return this.form.get('licenseNumber') as FormControl;
  }

  public get licenseCode(): FormControl {
    return this.form.get('licenseCode') as FormControl;
  }

  /**
   * @description
   * ID of a practitioner, but also known as prescriberId
   * when applied to nurses.
   */
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

  /**
   * @description
   * Handle changes to prescriber opt-in/out, but will only ever
   * occur when the prescriberIDType is optional.
   */
  public onPrescribing({ checked: isPrescribing }: MatSlideToggleChange): void {
    (isPrescribing)
      ? this.setPractitionerIdStateAndValidators(this.prescriberIdType, isPrescribing)
      : this.resetPractitionerIdStateAndValidators();
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
      const initialLicenceCode: number | null = +this.licenseCode?.value ?? null;
      this.licenseCode.valueChanges
        // Allow for initialization of the licence code when
        // the code already exists
        .pipe(startWith(initialLicenceCode))
        .subscribe((licenseCode: number) => {
          if (licenseCode) {
            this.setPractitionerInformation(licenseCode);
          }
        });

      const initialNursingCategory: number | null = +this.nurseCategory.value ?? null;
      this.nurseCategory.valueChanges
        .pipe(startWith(initialNursingCategory))
        .subscribe((collegeLicenseGroupingCode: number) =>
          this.loadLicensesByNursingCategory(collegeLicenseGroupingCode)
        );
    } else {
      const prescriberIdType = this.prescriberIdTypeByLicenceCode(this.licenseCode.value);
      const isPrescribing = prescriberIdType === PrescriberIdTypeEnum.Optional && !!this.practitionerId.value;
      this.setPractitionerIdStateAndValidators(prescriberIdType, isPrescribing);
    }
  }

  private setCollegeCertification(collegeCode: number): void {
    if (!collegeCode) {
      this.removeValidations();
      return;
    }

    if (collegeCode === CollegeLicenceClassEnum.BCCNM && !this.condensed) {
      this.formUtilsService.setValidators(this.nurseCategory, [Validators.required]);
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
    const licenseNumberValidators = [Validators.required];
    if (this.collegeCode.value === CollegeLicenceClassEnum.CPSBC) {
      licenseNumberValidators.push(FormControlValidators.numeric, FormControlValidators.requiredLength(5));
    } else {
      licenseNumberValidators.push(FormControlValidators.alphanumeric);
    }
    this.formUtilsService.setValidators(this.licenseNumber, licenseNumberValidators);

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
      this.resetPractitionerIdStateAndValidators();
    }
  }

  private setPractitionerInformation(licenseCode: number) {
    const prescriberIdType = this.prescriberIdTypeByLicenceCode(licenseCode);
    let isPrescribing = this.isPrescribing;

    switch (prescriberIdType) {
      case PrescriberIdTypeEnum.NA:
        // Ensures validators are cleared and value reset to prevent
        // values persisting through to submission
        this.resetPractitionerIdStateAndValidators();
        break;
      case PrescriberIdTypeEnum.Optional:
        // Maintain validators only if the value exists
        if (!this.practitionerId.value) {
          this.resetPractitionerIdStateAndValidators();
        }
        // Ensures that changes in licence code from mandatory
        // to optional will show the input
        isPrescribing = this.practitionerId.value;
        break;
      case PrescriberIdTypeEnum.Mandatory:
        break; // NOOP
    }

    this.setPractitionerIdStateAndValidators(prescriberIdType, isPrescribing);
  }

  private setPractitionerIdStateAndValidators(prescriberIdType: PrescriberIdTypeEnum, isPrescribing: boolean) {
    // Always set prescriber related values, but don't
    // update the validations unless not condensed
    this.prescriberIdType = prescriberIdType;
    this.isPrescribing = isPrescribing;

    if (
      !this.condensed &&
      (prescriberIdType === PrescriberIdTypeEnum.Mandatory || (isPrescribing && prescriberIdType !== PrescriberIdTypeEnum.NA))
    ) {
      this.formUtilsService.setValidators(this.practitionerId, [
        Validators.required,
        FormControlValidators.numeric,
        FormControlValidators.requiredLength(5)
      ]);
    }
  }

  private resetPractitionerIdStateAndValidators() {
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
      this.formUtilsService.setValidators(this.nurseCategory, []);
    }
  }

  private loadLicenses(collegeCode: number) {
    if (collegeCode !== CollegeLicenceClassEnum.BCCNM) {
      this.filteredLicenses = this.filterLicenses(collegeCode);
      this.licenseCode.patchValue(this.licenseCode.value || null, { emitEvent: false });
    }
  }

  private loadLicensesByNursingCategory(nursingCategory: number) {
    if (this.collegeCode.value === CollegeLicenceClassEnum.BCCNM) {
      this.filteredLicenses = this.filterLicensesByGrouping(nursingCategory);
      this.licenseCode.patchValue(this.licenseCode.value || null, { emitEvent: false });
    }
  }

  private loadPractices(collegeCode: number) {
    this.filteredPractices = this.filterPractices(collegeCode);
    this.practiceCode.patchValue(this.practiceCode.value || null);
    this.hasPractices = !!this.filteredPractices.length;
  }

  private filterLicenses(collegeCode: number): LicenseConfig[] {
    return this.licenses.filter(l => l.collegeLicenses.map(cl => cl.collegeCode).includes(collegeCode));
  }

  private filterLicensesByGrouping(collegeLicenseGroupingCode: number): LicenseConfig[] {
    return this.licenses.filter(l => l.collegeLicenses.map(cl => cl.collegeLicenseGroupingCode).includes(collegeLicenseGroupingCode));
  }

  private filterPractices(collegeCode: number): PracticeConfig[] {
    return this.practices.filter(p => p.collegePractices.map(cl => cl.collegeCode).includes(collegeCode));
  }
}
