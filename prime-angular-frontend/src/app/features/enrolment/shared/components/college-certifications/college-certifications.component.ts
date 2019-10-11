import { Component, OnInit, Input, Output, EventEmitter, ChangeDetectionStrategy } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { Config, CollegeConfig, LicenseConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ViewportService } from '@core/services/viewport.service';

@Component({
  selector: 'app-college-certifications',
  templateUrl: './college-certifications.component.html',
  styleUrls: ['./college-certifications.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CollegeCertificationsComponent implements OnInit {
  @Input() public form: FormGroup;
  @Output() public remove: EventEmitter<number>;

  public colleges: CollegeConfig[];
  public licenses: LicenseConfig[];
  public filteredLicenses: Config[];
  public licensePrefix: string;
  public practices: Config[];

  constructor(
    private configService: ConfigService,
    private viewportService: ViewportService
  ) {
    this.remove = new EventEmitter<number>();
    this.colleges = this.configService.colleges;
    this.licenses = this.configService.licenses;
    this.practices = this.configService.practices;
  }

  public get collegeCode(): FormControl {
    return this.form.get('collegeCode') as FormControl;
  }

  public get isMobile() {
    return this.viewportService.isMobile;
  }

  public onRemove() {
    this.remove.emit();
  }

  public ngOnInit() {
    this.filteredLicenses = this.filterLicenses(this.collegeCode.value);

    this.collegeCode.valueChanges.subscribe((collegeCode: number) => {
      this.filteredLicenses = this.filterLicenses(collegeCode);
      this.licensePrefix = this.colleges.filter(c => c.code === collegeCode).shift().prefix;

      this.form.get('licenseCode').patchValue(null);
    });
  }

  private filterLicenses(collegeCode: number): LicenseConfig[] {
    return this.licenses.filter(l => l.collegeLicenses.map(cl => cl.collegeCode).includes(collegeCode));
  }
}
