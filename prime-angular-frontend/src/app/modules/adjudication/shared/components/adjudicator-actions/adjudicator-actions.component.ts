import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges, OnChanges } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { noop } from 'rxjs';

import { EnumUtils } from '@lib/utils/enum-utils.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { AgreementType } from '@shared/enums/agreement-type.enum';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { EnrolleeListViewModel } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { EscalationNoteComponent, EscalationType } from '@shared/components/dialogs/content/escalation-note/escalation-note.component';
import { Role } from '@auth/shared/enum/role.enum';
import { PermissionService } from '@auth/shared/services/permission.service';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-adjudicator-actions',
  templateUrl: './adjudicator-actions.component.html',
  styleUrls: ['./adjudicator-actions.component.scss']
})
export class AdjudicatorActionsComponent implements OnInit, OnChanges {
  @Input() public enrollee: EnrolleeListViewModel;
  @Input() public isStacked: boolean = true;
  @Output() public approve: EventEmitter<{ enrolleeId: number, agreementName: string }>;
  @Output() public decline: EventEmitter<number>;
  @Output() public lock: EventEmitter<number>;
  @Output() public unlock: EventEmitter<number>;
  @Output() public enableEnrollee: EventEmitter<number>;
  @Output() public toggleManualAdj: EventEmitter<{ enrolleeId: number, alwaysManual: boolean }>;
  @Output() public enableEditing: EventEmitter<number>;
  @Output() public cancelToa: EventEmitter<number>;
  @Output() public rerunRules: EventEmitter<number>;
  @Output() public delete: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;
  @Output() public assignToa: EventEmitter<{ enrolleeId: number, agreementType: AgreementType }>;
  @Output() public reload: EventEmitter<boolean>;

  public form: FormGroup;
  public termsOfAccessAgreements: { type: AgreementType, name: string }[];

  public EnrolmentStatus = EnrolmentStatusEnum;
  public AdjudicationRoutes = AdjudicationRoutes;
  public Role = Role;


  constructor(
    private permissionService: PermissionService,
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog
  ) {
    this.approve = new EventEmitter<{ enrolleeId: number, agreementName: string }>();
    this.decline = new EventEmitter<number>();
    this.lock = new EventEmitter<number>();
    this.unlock = new EventEmitter<number>();
    this.enableEnrollee = new EventEmitter<number>();
    this.enableEditing = new EventEmitter<number>();
    this.cancelToa = new EventEmitter<number>();
    this.rerunRules = new EventEmitter<number>();
    this.delete = new EventEmitter<number>();
    this.assignToa = new EventEmitter<{ enrolleeId: number, agreementType: AgreementType }>();
    this.toggleManualAdj = new EventEmitter<{ enrolleeId: number, alwaysManual: boolean }>();
    this.route = new EventEmitter<string | (string | number)[]>();
    this.reload = new EventEmitter<boolean>();

    this.termsOfAccessAgreements = [
      { type: 0, name: 'None' },
      { type: AgreementType.REGULATED_USER_TOA, name: 'RU' },
      { type: AgreementType.OBO_TOA, name: 'OBO' },
      { type: AgreementType.COMMUNITY_PHARMACIST_TOA, name: 'PharmRU' },
      { type: AgreementType.PHARMACY_OBO_TOA, name: 'PharmOBO' }
    ];
  }

  public get assignedTOAType(): FormControl {
    return this.form.get('assignedTOAType') as FormControl;
  }

  public get isUnderReview(): boolean {
    return (this.enrollee && this.enrollee.currentStatusCode === EnrolmentStatusEnum.UNDER_REVIEW);
  }

  public onApprove() {
    if (this.formUtilsService.checkValidity(this.form) &&
      this.permissionService.hasRoles(Role.APPROVE_ENROLLEE) && this.isUnderReview) {
      const agreementName = this.termsOfAccessAgreements
        .filter(t => t.type === this.assignedTOAType.value)[0]
        .name;
      this.approve.emit({ enrolleeId: this.enrollee.id, agreementName });
    }
  }

  public onDecline() {
    if (this.permissionService.hasRoles(Role.MANAGE_ENROLLEE)) {
      this.decline.emit(this.enrollee.id);
    }
  }

  public onLock() {
    if (this.permissionService.hasRoles(Role.MANAGE_ENROLLEE)) {
      this.lock.emit(this.enrollee.id);
    }
  }

  public onUnlock() {
    if (this.permissionService.hasRoles(Role.MANAGE_ENROLLEE)) {
      this.unlock.emit(this.enrollee.id);
    }
  }

  public onDelete() {
    if (this.permissionService.hasRoles(Role.MANAGE_ENROLLEE)) {
      this.delete.emit(this.enrollee.id);
    }
  }

  public onEnableEnrollee() {
    if (this.permissionService.hasRoles(Role.MANAGE_ENROLLEE)) {
      this.enableEnrollee.emit(this.enrollee.id);
    }
  }

  public onEnableEditing() {
    if (this.permissionService.hasRoles(Role.APPROVE_ENROLLEE)) {
      this.enableEditing.emit(this.enrollee.id);
    }
  }

  public onCancelToa() {
    if (this.permissionService.hasRoles(Role.APPROVE_ENROLLEE)) {
      this.cancelToa.emit(this.enrollee.id);
    }
  }

  public onRerunRules() {
    if (this.permissionService.hasRoles(Role.TRIAGE_ENROLLEE)) {
      this.rerunRules.emit(this.enrollee.id);
    }
  }

  public onToggleManualAdj() {
    if (this.permissionService.hasRoles(Role.MANAGE_ENROLLEE)) {
      this.toggleManualAdj.emit({
        enrolleeId: this.enrollee.id,
        alwaysManual: !this.enrollee.alwaysManual
      });
    }
  }

  public onEscalate() {
    const data: DialogOptions = {
      data: {
        id: this.enrollee.id,
        escalationType: EscalationType.ENROLLEE
      }
    };

    this.dialog.open(EscalationNoteComponent, { data }).afterClosed()
      .subscribe((result: { reload: boolean }) => (result?.reload) ? this.reload.emit(true) : noop);
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (this.form && changes.enrollee) {
      // Update selector value when parent changes value
      if (changes.enrollee.currentValue?.assignedTOAType !== changes.enrollee.previousValue?.assignedTOAType) {
        this.assignedTOAType.patchValue(changes.enrollee.currentValue?.assignedTOAType ?? 0, { emitEvent: false });
      }

      // Disable or enable based on enrollee status
      if (changes.enrollee.currentValue.currentStatusCode === EnrolmentStatusEnum.UNDER_REVIEW) {
        this.assignedTOAType.enable({ emitEvent: false });
      } else {
        this.assignedTOAType.disable({ emitEvent: false });
      }
    }
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      assignedTOAType: [
        { value: 0, disabled: !this.isUnderReview },
        [FormControlValidators.requiredIn(EnumUtils.values(AgreementType))]
      ]
    });
  }

  private initForm() {
    this.assignedTOAType.patchValue(this.enrollee?.assignedTOAType ?? 0);

    this.assignedTOAType.valueChanges
      .subscribe((agreementType: AgreementType) =>
        this.assignToa.emit({ enrolleeId: this.enrollee.id, agreementType })
      );
  }
}
