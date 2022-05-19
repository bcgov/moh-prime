import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import moment from 'moment';

import { FormUtilsService } from '@core/services/form-utils.service';
import { ViewportService } from '@core/services/viewport.service';
import { RegulatoryFormState } from '@paper-enrolment/pages/regulatory-page/regulatory-form-state.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';

@Component({
  selector: 'app-unlisted-college-licence-form',
  templateUrl: './unlisted-college-licence-form.component.html',
  styleUrls: ['./unlisted-college-licence-form.component.scss']
})
export class UnlistedCollegeLicenceFormComponent implements OnInit, OnChanges {
  @Input() public form: FormGroup;
  @Input() public formState: RegulatoryFormState;
  @Input() public index: number;
  @Input() public total: number;
  @Input() public validate: boolean;
  @Input() public formControlNames: string[];
  @Output() public remove: EventEmitter<number>;
  public minRenewalDate: moment.Moment;

  constructor(
    private viewportService: ViewportService,
    private formUtilsService: FormUtilsService
  ) {
    this.remove = new EventEmitter<number>();
    this.minRenewalDate = moment();
    this.validate = false;
  }

  public get isMobile() {
    return this.viewportService.isMobile;
  }

  public get unlistedCollegeName(): FormControl {
    return this.form.get('unlistedCollegeName') as FormControl;
  }

  public get unlistedCollegeCode(): FormControl {
    return this.form.get('unlistedCollegeCode') as FormControl;
  }

  public get unlistedRenewalDate(): FormControl {
    return this.form.get('unlistedRenewalDate') as FormControl;
  }

  public removeUnlistedCertification(): void {
    this.remove.emit(this.index)
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (this.form && changes && !this.validate) {
      this.removeValidations();
    }
  }

  public ngOnInit(): void {
    if (this.validate) {
      this.setUnlistedCollegeCertificationValidators()
    } else {
      this.removeValidations();
    }
  }

  private setUnlistedCollegeCertificationValidators(): void {
      this.formUtilsService.setValidators(this.unlistedCollegeName, [Validators.required]);
      this.formUtilsService.setValidators(this.unlistedCollegeCode, [
        Validators.required,
        FormControlValidators.numeric,
        FormControlValidators.requiredLength(5)
      ]);
      this.formUtilsService.setValidators(this.unlistedRenewalDate, [Validators.required]);

  }

  private removeValidations(): void {
    this.formState.form.markAsPristine();
    this.formUtilsService.setValidators(this.unlistedCollegeName, []);
    this.formUtilsService.setValidators(this.unlistedCollegeCode, []);
    this.formUtilsService.setValidators(this.unlistedRenewalDate, []);
  }
}
