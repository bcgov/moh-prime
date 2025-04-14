import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { FormUtilsService } from '@core/services/form-utils.service';
import { MINIMUM_AGE } from '@lib/constants';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import moment from 'moment';
import { debounceTime } from 'rxjs/operators';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-date-of-birth',
  templateUrl: './date-of-birth.component.html',
  styleUrls: ['./date-of-birth.component.scss']
})
export class DateOfBirthComponent implements OnInit {
  @Output() public output: EventEmitter<string>;

  public form: UntypedFormGroup;
  public isEmpty: boolean;
  public enrollee: HttpEnrollee;
  public maxDateOfBirth: moment.Moment;
  public outputValue: string;



  constructor(
    private fb: UntypedFormBuilder,
    private formUtilsService: FormUtilsService,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
  ) {
    this.output = new EventEmitter<string>();

    // Must be 18 years of age or older
    this.maxDateOfBirth = moment().subtract(MINIMUM_AGE, 'years');
  }

  @Input() public set data({ enrollee }: { enrollee: HttpEnrollee }) {
    this.enrollee = enrollee;
  }

  public get dateOfBirth(): UntypedFormControl {
    return this.form.get('dateOfBirth') as UntypedFormControl;
  }

  public onSave() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const response = { output: this.dateOfBirth.value };
      this.dialogRef.close(response);
    }
    this.form.markAsDirty();
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      dateOfBirth: ['', [Validators.required]]
    });
  }

  private initForm() {
    this.dateOfBirth.patchValue(this.enrollee?.dateOfBirth);
  }
}
