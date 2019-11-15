import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { Config, CollegeConfig, LicenseConfig, PracticeConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ViewportService } from '@core/services/viewport.service';
import { FormUtilsService } from '@enrolment/shared/services/form-utils.service';
import { FormControlValidators } from '@shared/validators/form-control.validators';

@Component({
  selector: 'app-college-certifications',
  templateUrl: './college-certifications.component.html',
  styleUrls: ['./college-certifications.component.scss']
})
export class CollegeCertificationsComponent implements OnInit {
  @Input() public form: FormGroup;
  @Input() public index: number;
  @Input() public total: number;
  @Output() public remove: EventEmitter<number>;

  public colleges: CollegeConfig[];
  public licenses: LicenseConfig[];
  public practices: PracticeConfig[];
  public filteredLicenses: Config<number>[];
  public filteredPractices: Config<number>[];
  public hasPractices: boolean;
  public licensePrefix: string;

  constructor(
    private configService: ConfigService,
    private viewportService: ViewportService,
    private formUtilsService: FormUtilsService
  ) {
    this.remove = new EventEmitter<number>();
    this.colleges = this.configService.colleges;
    this.licenses = this.configService.licenses;
    this.practices = this.configService.practices;
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

  public onRemove() {
    this.remove.emit(this.index);
  }

  public ngOnInit() {
    if (this.collegeCode.value) {
      this.setValidation();
      this.loadLicenses(this.collegeCode.value);
      this.loadPractices(this.collegeCode.value);
    }

    // TODO Refactor so value changes is triggered when form is patched
    this.collegeCode.valueChanges.subscribe((collegeCode: number) => {
      if (collegeCode) {
        this.setValidation();
        this.loadLicenses(collegeCode);
        this.loadPractices(collegeCode);
      }
    });
  }

  private filterLicenses(collegeCode: number): LicenseConfig[] {
    return this.licenses.filter(l => l.collegeLicenses.map(cl => cl.collegeCode).includes(collegeCode));
  }

  private filterPractices(collegeCode: number): PracticeConfig[] {
    return this.practices.filter(p => p.collegePractices.map(cl => cl.collegeCode).includes(collegeCode));
  }

  private loadLicenses(collegeCode: number) {
    this.filteredLicenses = this.filterLicenses(collegeCode);
    this.licensePrefix = this.colleges.filter(c => c.code === collegeCode).shift().prefix;
    this.licenseCode.patchValue(this.licenseCode.value || null);
  }

  private loadPractices(collegeCode: number) {
    this.filteredPractices = this.filterPractices(collegeCode);
    this.practiceCode.patchValue(this.practiceCode.value || null);
    this.hasPractices = (this.filteredPractices.length) ? true : false;
  }

  private setValidation() {
    this.formUtilsService.setValidators(this.licenseNumber, [Validators.required, FormControlValidators.requiredLength(5)]);
    this.formUtilsService.setValidators(this.licenseCode, [Validators.required]);
    this.formUtilsService.setValidators(this.renewalDate, [Validators.required]);
  }
}
