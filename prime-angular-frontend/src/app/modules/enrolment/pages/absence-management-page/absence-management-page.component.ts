import { Component, OnInit } from '@angular/core';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolleeAbsence } from '@shared/models/enrollee-absence.model';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { Observable, Subscription } from 'rxjs';
import moment, { Moment } from 'moment';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AbsenceManagementFormState } from './absence-management-form-state.class';
import { MatDialog } from '@angular/material/dialog';
import { FormUtilsService } from '@core/services/form-utils.service';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormBuilder } from '@angular/forms';
import { NoContent } from '@core/resources/abstract-resource';

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
    private formatDatePipe: FormatDatePipe,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private route: ActivatedRoute,
    private router: Router
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

    return this.enrolmentResource
      .createEnrolleeAbsence(this.enrolmentService.enrolment.id, payload.start, payload.end)
  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.form.reset();
    this.getEnrolleeAbsence();
  }

  private getEnrolleeAbsence(): void {
    const enrolleeId = this.enrolmentService.enrolment.id;
    this.busy = this.enrolmentResource.getEnrolleeAbsence(enrolleeId)
      .subscribe((absence: EnrolleeAbsence) => this.absence = absence)
  }

}
