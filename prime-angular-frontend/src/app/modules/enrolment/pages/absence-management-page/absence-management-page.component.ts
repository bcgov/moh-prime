import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';
import { EMPTY, noop, Observable, of, Subscription } from 'rxjs';
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
    private fb: FormBuilder,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private formatDatePipe: FormatDatePipe
  ) {
    super(dialog, formUtils);
  }

  public isCurrent(): boolean {
    return moment().isBetween(this.absence?.startTimestamp, this.absence?.endTimestamp);
  }

  public endAbsence(): void {
    this.busy = this.enrolmentResource.endEnrolleeAbsence(this.enrolmentService.enrolment.id)
      .subscribe(() => this.absence = null);
  }

  public cancelAbsence(absenceId: number): void {
    this.busy = this.enrolmentResource.deleteFutureEnrolleeAbsence(this.enrolmentService.enrolment.id, absenceId)
      .subscribe(() => this.absence = null);
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
    const payload = this.formState.json;

    if (moment().isAfter(payload.end)) {
      const data: DialogOptions = {
        title: 'Publish Past Absence',
        message: `Are you sure you want to publish an absence from ${this.formatDatePipe.transform(payload.start)}
          - ${this.formatDatePipe.transform(payload.end)}. Once a past absence is created, it cannot be changed or removed.`,
        actionText: 'Publish'
      };

      return this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: any) => {
            if (result) {
              return this.enrolmentResource
                .createEnrolleeAbsence(this.enrolmentService.enrolment.id, payload.start, payload.end);
            }
            return EMPTY;
          }),
        );
    }

    return this.enrolmentResource
      .createEnrolleeAbsence(this.enrolmentService.enrolment.id, payload.start, payload.end);

  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.form.reset();
    this.getEnrolleeAbsence();
  }

  private getEnrolleeAbsence(): void {
    const enrolleeId = this.enrolmentService.enrolment.id;
    this.busy = this.enrolmentResource.getEnrolleeAbsence(enrolleeId)
      .subscribe((absence: EnrolleeAbsence) => this.absence = absence);
  }

}
