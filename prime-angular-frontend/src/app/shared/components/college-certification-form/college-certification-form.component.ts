import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import * as moment from 'moment';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { Config, CollegeConfig, LicenseConfig, PracticeConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ViewportService } from '@core/services/viewport.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CollegeLicenceClass } from '@shared/enums/college-licence-class.enum';

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
  @Input() public condensed: boolean;
  @Output() public remove: EventEmitter<number>;

  public colleges: CollegeConfig[];
  public licenses: LicenseConfig[];
  public practices: PracticeConfig[];
  public filteredLicenses: Config<number>[];
  public filteredPractices: Config<number>[];
  public hasPractices: boolean;
  public licensePrefix: string;
  public minRenewalDate: moment.Moment;
  public CollegeLicenceClass = CollegeLicenceClass;

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

  // Only show College of Physicians and Surgeons or College or Nurses for remote user cert.
  public getDisplayedColleges(): CollegeConfig[] {
    return this.filteredColleges
      .filter(c => !this.condensed || (c.code === CollegeLicenceClass.CPSBC || c.code === CollegeLicenceClass.BCCNM));
  }

  public removeCertification() {
    this.remove.emit(this.index);
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
  }

  private setCollegeCertification(collegeCode: number): void {
    if (!collegeCode) {
      this.removeValidations();
      return;
    }

    // Initialize the validations when the college code is not
    // "None" to allow for submission when no college is selected
    this.setValidations();
    this.setPrefix(collegeCode);

    this.loadLicenses(collegeCode);
    if (this.filteredLicenses?.length === 1) {
      this.licenseCode.patchValue(this.filteredLicenses[0].code);
    }

    if (!this.condensed) {
      this.loadPractices(collegeCode);
    }
  }

  private setValidations() {
    this.formUtilsService.setValidators(this.licenseNumber, [Validators.required, FormControlValidators.alphanumeric]);
    this.formUtilsService.setValidators(this.licenseCode, [Validators.required]);

    if (!this.condensed) {
      this.formUtilsService.setValidators(this.renewalDate, [Validators.required]);
    }
  }

  private resetCollegeCertification() {
    this.licenseNumber.reset(null);
    this.licenseCode.reset(null);

    if (!this.condensed) {
      this.renewalDate.reset(null);
      this.practiceCode.reset(null);
    }
  }

  private removeValidations() {
    this.formUtilsService.setValidators(this.licenseNumber, []);
    this.formUtilsService.setValidators(this.licenseCode, []);

    if (!this.condensed) {
      this.formUtilsService.setValidators(this.renewalDate, []);
    }
  }

  private setPrefix(collegeCode: number) {
    this.licensePrefix = this.colleges.filter(c => c.code === collegeCode).shift().prefix || 'N/A';
  }

  private loadLicenses(collegeCode: number) {
    this.filteredLicenses = this.filterLicenses(collegeCode);
    this.licenseCode.patchValue(this.licenseCode.value || null);
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
