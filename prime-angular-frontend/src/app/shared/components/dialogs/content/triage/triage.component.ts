import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { noop, Observable, of, Subscription } from 'rxjs';

import { EmailUtils } from '@lib/utils/email-utils.class';
import { ToastService } from '@core/services/toast.service';
import { EnrolleeStatusAction } from '@shared/enums/enrollee-status-action.enum';
import { EnrolleeListViewModel } from '@shared/models/enrolment.model';
import { EnrolmentStatusAdmin } from '@shared/models/enrolment-status-admin.model';
import { Role } from '@auth/shared/enum/role.enum';

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
  @Input() public enrollee: EnrolleeListViewModel;
  @Input() public assigned: boolean;
  @Output() public reload: EventEmitter<void>;

  public busy: Subscription;
  public status$: Observable<EnrolmentStatusAdmin>;
  public Role = Role;

  constructor(
    private enrolmentResource: EnrolmentResource,
    private adjudicationResource: AdjudicationResource,
    private toastService: ToastService,
    private dialog: MatDialog,
  ) {
    this.reload = new EventEmitter<void>();
    this.assigned = false;
  }

  public onOpen() {
    this.getCurrentStatus();
  }

  public onEscalate() {
    const data: DialogOptions = {
      data: {
        id: this.enrollee.id,
        escalationType: EscalationType.ENROLLEE
      }
    };

    this.dialog.open(EscalationNoteComponent, { data }).afterClosed()
      .subscribe((result: { reload: boolean }) =>
        (result?.reload)
          ? this.reload.emit()
          : noop
      );
  }

  public onEnableEditing() {
    this.adjudicationResource.enrolleeStatusAction(this.enrollee.id, EnrolleeStatusAction.ENABLE_EDITING)
      .subscribe(() => this.reload.emit());
  }

  public onRerunRules() {
    this.adjudicationResource.enrolleeStatusAction(this.enrollee.id, EnrolleeStatusAction.RERUN_RULES)
      .subscribe(() => this.reload.emit());
  }

  public onNotify() {
    if (!this.enrollee.email) {
      this.toastService.openErrorToast('Enrollee does not have a contact email.');
      return;
    }
    EmailUtils.openEmailClient(this.enrollee.email);
  }

  public ngOnInit(): void { }

  private getCurrentStatus(): void {
    this.status$ = this.enrolmentResource.getCurrentStatus(this.enrollee.id);
  }
}
