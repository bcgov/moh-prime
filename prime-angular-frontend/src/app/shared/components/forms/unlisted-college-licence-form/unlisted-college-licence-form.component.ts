import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import moment from 'moment';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { ViewportService } from '@core/services/viewport.service';
import { RegulatoryFormState } from '@paper-enrolment/pages/regulatory-page/regulatory-form-state.class';

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

  public get collegeName(): FormControl {
    return this.form.get('collegeName') as FormControl;
  }

  public get licenceNumber(): FormControl {
    return this.form.get('licenceNumber') as FormControl;
  }

  public get renewalDate(): FormControl {
    return this.form.get('renewalDate') as FormControl;
  }

  public get licenceClass(): FormControl {
    return this.form.get('licenceClass') as FormControl;
  }

  public removeUnlistedCertification(): void {
    this.remove.emit(this.index);
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (this.form) {
      if (this.validate) {
        this.setUnlistedCollegeCertificationValidators()
      } else {
        this.removeValidations();
      }
    }
  }

  public ngOnInit(): void {
  }

  private setUnlistedCollegeCertificationValidators(): void {
    this.formUtilsService.setValidators(this.collegeName, [Validators.required]);
    this.formUtilsService.setValidators(this.licenceNumber, [
      Validators.required,
      FormControlValidators.alphanumeric
    ]);
    this.formUtilsService.setValidators(this.renewalDate, [Validators.required]);
    this.formUtilsService.setValidators(this.licenceClass, [Validators.required]);
  }

  private removeValidations(): void {
    this.form.markAsPristine();
    this.formUtilsService.setValidators(this.collegeName, []);
    this.formUtilsService.setValidators(this.licenceNumber, []);
    this.formUtilsService.setValidators(this.renewalDate, []);
    this.formUtilsService.setValidators(this.licenceClass, []);
  }
}
