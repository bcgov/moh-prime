import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

import moment from 'moment';

import { ViewportService } from '@core/services/viewport.service';
import { RegulatoryFormState } from '@paper-enrolment/pages/regulatory-page/regulatory-form-state.class';
import { UnlistedCertification } from '@paper-enrolment/shared/models/unlisted-certification.model';

@Component({
  selector: 'app-unlisted-college-licence-form',
  templateUrl: './unlisted-college-licence-form.component.html',
  styleUrls: ['./unlisted-college-licence-form.component.scss']
})
export class UnlistedCollegeLicenceFormComponent implements OnInit {
  @Input() public form: FormGroup;
  @Input() public index: number;
  @Input() public formControlNames: string[];
  public formState: RegulatoryFormState;
  public minRenewalDate: moment.Moment;

  constructor(
    private viewportService: ViewportService
  ) {
    this.formControlNames = [
      'unlistedCollegeName',
      'unlistedCollegeName',
      'unlistedRenewalDate'
    ];
    this.minRenewalDate = moment();
  }

  public get isMobile() {
    return this.viewportService.isMobile;
  }

  public get unlistedCollegeName(): FormControl {
    return this.form.get('unlistedCollegeName') as FormControl;
  }

  public get unlistedCollegeLicence(): FormControl {
    return this.form.get('unlistedCollegeLicence') as FormControl;
  }

  public get unlistedRenewalDate(): FormControl {
    return this.form.get('unlistedRenewalDate') as FormControl;
  }

  ngOnInit(): void {
    // this.createFormInstance();
  }

  // private createFormInstance(): void {
  //   this.formState.buildForm();
  // }
}
