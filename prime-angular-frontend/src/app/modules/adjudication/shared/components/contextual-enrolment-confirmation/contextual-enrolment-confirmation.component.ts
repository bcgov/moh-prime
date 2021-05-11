import { noop, Observable, Subscription } from 'rxjs';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';

import { Role } from '@auth/shared/enum/role.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EscalationNoteComponent, EscalationType } from '@shared/components/dialogs/content/escalation-note/escalation-note.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { FormBuilder, FormGroup } from '@angular/forms';


@Component({
  selector: 'app-contextual-enrolment-confirmation',
  templateUrl: './contextual-enrolment-confirmation.component.html',
  styleUrls: ['./contextual-enrolment-confirmation.component.scss']
})
export class ContextualEnrolmentConfirmationComponent implements OnInit {

  @Input() public enrolleeId: number;
  @Input() public assigned: boolean;
  @Output() public reload: EventEmitter<void>;

  public busy: Subscription;
  public status$: Observable<EnrolmentStatus>;
  public Role = Role;
  public form: FormGroup;

  constructor(
    private enrolmentResource: EnrolmentResource,
    private authService: AuthService,
    private adjudicationResource: AdjudicationResource,
    private toastService: ToastService,
    private utilsService: UtilsService,
    private dialog: MatDialog,
    private fb: FormBuilder
  ) {
    this.reload = new EventEmitter<void>();
    this.assigned = false;
  }

  public onOpen() {
    this.getCurrentStatus();
  }

  public onSubmit() {

  }

  public onEscalate() {
    const data: DialogOptions = {
      data: {
        id: this.enrolleeId,
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

  public ngOnInit(): void {
    this.form = this.fb.group({
      nameinDirectory: this.fb.control,
      phoneInBC: this.fb.control
    });
  }

  private getCurrentStatus() {
    this.status$ = this.enrolmentResource.getCurrentStatus(this.enrolleeId);
  }

}
