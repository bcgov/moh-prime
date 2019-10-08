import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { ConfigKeyValue } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ViewportService } from '@core/services/viewport.service';

@Component({
  selector: 'app-college-certifications',
  templateUrl: './college-certifications.component.html',
  styleUrls: ['./college-certifications.component.scss']
})
export class CollegeCertificationsComponent implements OnInit {
  @Input() public form: FormGroup;
  @Output() public remove: EventEmitter<number>;

  public colleges: ConfigKeyValue[];
  public licenses: ConfigKeyValue[];
  public filteredLicenses: ConfigKeyValue[];
  public licensePrefix: number;
  public advancedPractices: ConfigKeyValue[];

  constructor(
    private configService: ConfigService,
    private viewportService: ViewportService
  ) {
    this.remove = new EventEmitter<number>();
    this.colleges = this.configService.colleges;
    this.licenses = this.configService.licenses;
    this.advancedPractices = this.configService.advancedPractices;
  }

  public get isMobile() {
    return this.viewportService.isMobile;
  }

  public onRemove() {
    this.remove.emit();
  }

  public ngOnInit() {
    // TODO: add a test to check that prefix and licenses for a college are correct
    this.form.get('collegeCode').valueChanges.subscribe((collegeCode) => {
      this.filteredLicenses = this.licenses.filter(l => l.collegeCode === collegeCode);
      this.licensePrefix = this.colleges.filter(c => c.code === collegeCode).shift().prefix;

      this.form.get('licenseCode').patchValue(null);
    });
  }
}
