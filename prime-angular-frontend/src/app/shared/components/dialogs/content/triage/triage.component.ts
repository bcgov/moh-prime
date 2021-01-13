import { Component, Input, OnInit } from '@angular/core';

import { EMPTY, Observable, of, Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { SubmissionAction } from '@shared/enums/submission-action.enum';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { HttpEnrollee } from '@shared/models/enrolment.model';

import { AuthService } from '@auth/shared/services/auth.service';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

@Component({
  selector: 'app-triage',
  templateUrl: './triage.component.html',
  styleUrls: ['./triage.component.scss']
})
export class TriageComponent implements OnInit {
  @Input() public enrolleeId: number;
  public busy: Subscription;
  public status$: Observable<EnrolmentStatus>;

  constructor(
    public enrolmentResource: EnrolmentResource,
    public authService: AuthService,
    public adjudicationResource: AdjudicationResource,
    public toastService: ToastService,
    public utilsService: UtilsService,
  ) {
  }

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public onEscalate() {
    // TODO: Part of escalate ticket.
  }

  public onEnableEditing() {
    this.adjudicationResource.submissionAction(this.enrolleeId, SubmissionAction.ENABLE_EDITING)
      .subscribe();
  }

  public onRerunRules() {
    this.adjudicationResource.submissionAction(this.enrolleeId, SubmissionAction.RERUN_RULES)
      .subscribe();
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

  ngOnInit(): void {
    this.getCurrentStatus();
  }

  private getCurrentStatus() {
    this.status$ = this.enrolmentResource.getCurrentStatus(this.enrolleeId);
  }

}
