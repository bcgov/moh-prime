import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';

import { FormUtilsService } from '@core/services/form-utils.service';
import { ViewportService } from '@core/services/viewport.service';

import { NextStepsFormState } from '@paper-enrolment/pages/next-steps-page/next-steps-form-state.class';
@Component({
  selector: 'app-email-form',
  templateUrl: './email-form.component.html',
  styleUrls: ['./email-form.component.scss']
})
export class EmailFormComponent implements OnInit {

  @Input() public form: UntypedFormGroup;
  @Input() public formState: NextStepsFormState;
  @Input() public showRemoveButton: boolean = true;
  @Input() public index: number;
  @Input() public validateFormat: boolean = false;
  @Input() public required: boolean = false;
  @Input() public label: string = 'Email';
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

  public get email(): UntypedFormControl {
    return this.form.get('email') as UntypedFormControl;
  }

  public removeEmailInput(): void {
    this.remove.emit(this.index);
  }

  public ngOnInit(): void {
    if (this.validateFormat) {
      this.setEmailFormatValidator();
    }
    if (this.required) {
      this.setRequireValidator();
    }
  }

  private setEmailFormatValidator(): void {
    this.formUtilsService.setValidators(this.email, [Validators.email])
  }

  private setRequireValidator(): void {
    this.formUtilsService.setValidators(this.email, [Validators.required])
  }

}
