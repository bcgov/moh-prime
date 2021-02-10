import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY, noop, Observable, of, Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { SubmissionAction } from '@shared/enums/submission-action.enum';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { Role } from '@auth/shared/enum/role.enum';

import { AuthService } from '@auth/shared/services/auth.service';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EscalationNoteComponent, EscalationType } from '../escalation-note/escalation-note.component';
import { DialogOptions } from '../../dialog-options.model';

@Component({
  selector: 'app-triage',
  templateUrl: './triage.component.html',
  styleUrls: ['./triage.component.scss']
})
export class TriageComponent implements OnInit {
  @Input() public enrolleeId: number;
  @Input() public assigned: boolean;
  @Output() public reload: EventEmitter<boolean>;

  public busy: Subscription;
  public status$: Observable<EnrolmentStatus>;
  public Role = Role;

  constructor(
    private enrolmentResource: EnrolmentResource,
    private authService: AuthService,
    private adjudicationResource: AdjudicationResource,
    private toastService: ToastService,
    private utilsService: UtilsService,
    private dialog: MatDialog,
  ) {
    this.reload = new EventEmitter<boolean>();
    this.assigned = false;
  }

  public onEscalate() {
    const data: DialogOptions = {
      data: {
        id: this.enrolleeId,
        escalationType: EscalationType.ENROLLEE
      }
    };

    this.dialog.open(EscalationNoteComponent, { data }).afterClosed()
      .subscribe((result: { reload: boolean }) => (result?.reload) ? this.reload.emit(true) : noop);
  }

  public onEnableEditing() {
    this.adjudicationResource.submissionAction(this.enrolleeId, SubmissionAction.ENABLE_EDITING)
      .subscribe(() => this.reload.emit(true));
  }

  public onRerunRules() {
    this.adjudicationResource.submissionAction(this.enrolleeId, SubmissionAction.RERUN_RULES)
      .subscribe(() => this.reload.emit(true));
  }

  public onNotify() {
    this.adjudicationResource.getEnrolleeById(this.enrolleeId)
      .pipe(
        exhaustMap((enrollee: HttpEnrollee) => {
          if (enrollee.email) {
            return of(enrollee);
          }
          this.toastService.openErrorToast('Enrollee does not have a contact email.');
          return EMPTY;
        }),
        exhaustMap((enrollee: HttpEnrollee) =>
          this.adjudicationResource.createInitiatedEnrolleeEmailEvent(enrollee.id)
            .pipe(map(() => enrollee))
        )
      )
      .subscribe((enrollee: HttpEnrollee) => {
        this.utilsService.mailTo(enrollee.email);
      });
  }

  public ngOnInit(): void {
    this.getCurrentStatus();
  }

  private getCurrentStatus() {
    this.status$ = this.enrolmentResource.getCurrentStatus(this.enrolleeId);
  }
}
