import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UntypedFormBuilder, Validators } from '@angular/forms';
import { EMPTY, Observable, Subscription } from 'rxjs';
import moment from 'moment';

import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent } from '@core/resources/abstract-resource';

import { EnrolleeAbsence } from '@shared/models/enrollee-absence.model';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';

import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

import { AbsenceManagementFormState } from './absence-management-form-state.class';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { exhaustMap } from 'rxjs/operators';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';

@Component({
  selector: 'app-absence-management-page',
  templateUrl: './absence-management-page.component.html',
  styleUrls: ['./absence-management-page.component.scss'],
  providers: [FormatDatePipe]
})
export class AbsenceManagementPageComponent extends AbstractEnrolmentPage implements OnInit {
  public busy: Subscription;
  public absence: EnrolleeAbsence;

  public formState: AbsenceManagementFormState;

  constructor(
    protected formUtils: FormUtilsService,
    protected dialog: MatDialog,
    private fb: UntypedFormBuilder,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private formatDatePipe: FormatDatePipe
  ) {
    super(dialog, formUtils);
  }

  public isCurrent(): boolean {
    return moment().isAfter(this.absence?.startTimestamp);
  }

  public endAbsence(): void {
    this.busy = this.enrolmentResource.endCurrentEnrolleeAbsence(this.enrolmentService.enrolment.id)
      .subscribe(() => {
        this.absence = null;
        this.formState.email.removeValidators(Validators.required);
      });
  }

  public cancelAbsence(absenceId: number): void {
    this.busy = this.enrolmentResource.deleteFutureEnrolleeAbsence(this.enrolmentService.enrolment.id, absenceId)
      .subscribe(() => {
        this.absence = null;
        this.formState.email.removeValidators(Validators.required);
      });
  }

  public sendEmail(): void {
    this.formState.email.markAsTouched();
    if (!this.formState.email.valid) {
      return;
    }
    const email = this.formState.json.email;
    this.busy = this.enrolmentResource.sendEnrolleeAbsenceEmail(this.enrolmentService.enrolment.id, email)
      .subscribe();
  }

  public ngOnInit(): void {
    this.getEnrolleeAbsence();
    this.createFormInstance();
  }

  protected createFormInstance(): void {
    this.formState = new AbsenceManagementFormState(this.fb);
  }

  protected patchForm(): void {
    // This form currently never requires patching.
  }

  protected performSubmission(): Observable<NoContent> {
    this.formState.form.markAsPristine();
    const { end, start } = this.formState.json;
    const endOfDay = moment(end).endOf('day').toISOString();

    if (moment().isAfter(endOfDay, 'date')) {
      const data: DialogOptions = {
        title: 'Publish Past Absence',
        message: `Are you sure you want to publish an absence from ${this.formatDatePipe.transform(start)}
          - ${this.formatDatePipe.transform(endOfDay)}. Once a past absence is created, it cannot be changed or removed.`,
        actionText: 'Publish'
      };

      return this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: any) => {
            if (result) {
              return this.enrolmentResource
                .createEnrolleeAbsence(this.enrolmentService.enrolment.id, start, endOfDay);
            }
            return EMPTY;
          }),
        );
    }

    return this.enrolmentResource
      .createEnrolleeAbsence(this.enrolmentService.enrolment.id, start, endOfDay);

  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.form.reset();
    this.getEnrolleeAbsence();
  }

  private getEnrolleeAbsence(): void {
    const enrolleeId = this.enrolmentService.enrolment.id;
    this.busy = this.enrolmentResource.getEnrolleeAbsences(enrolleeId)
      .subscribe((absences: EnrolleeAbsence[]) => {
        this.absence = absences?.[0];
        if (this.absence) {
          this.formState.email.addValidators(Validators.required);
        }
      });
  }

}
