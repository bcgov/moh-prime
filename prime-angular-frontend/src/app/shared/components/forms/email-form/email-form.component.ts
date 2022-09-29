import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { FormUtilsService } from '@core/services/form-utils.service';
import { ViewportService } from '@core/services/viewport.service';
import { NextStepsFormState } from '@paper-enrolment/pages/next-steps-page/next-steps-form-state.class';
import moment from 'moment';

@Component({
  selector: 'app-email-form',
  templateUrl: './email-form.component.html',
  styleUrls: ['./email-form.component.scss']
})
export class EmailFormComponent implements OnInit {

  @Input() public form: FormGroup;
  @Input() public formState: NextStepsFormState;
  @Input() public index: number;
  @Input() public validate: boolean = false;
  @Output() public remove: EventEmitter<number>;

  constructor(
    private viewportService: ViewportService,
    private formUtilsService: FormUtilsService
  ) {
    this.remove = new EventEmitter<number>();
  }

  public get isMobile() {
    return this.viewportService.isMobile;
  }

  public get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  public removeEmailInput(): void {
    this.remove.emit(this.index);
  }

  public ngOnInit(): void {
    if (this.validate) {
      this.setEmailValidators();
    }
  }

  private setEmailValidators(): void {
    this.formUtilsService.setValidators(this.email, [Validators.required, Validators.email])
  }

}
