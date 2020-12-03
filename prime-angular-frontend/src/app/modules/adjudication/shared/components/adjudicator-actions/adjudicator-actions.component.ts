import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { EnumUtils } from '@lib/utils/enum-utils.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { AgreementType } from '@shared/enums/agreement-type.enum';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { EnrolleeListViewModel } from '@shared/models/enrolment.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-adjudicator-actions',
  templateUrl: './adjudicator-actions.component.html',
  styleUrls: ['./adjudicator-actions.component.scss']
})
export class AdjudicatorActionsComponent implements OnInit {
  @Input() public enrollee: EnrolleeListViewModel;
  @Output() public approve: EventEmitter<{ enrolleeId: number, agreementName: string }>;
  @Output() public decline: EventEmitter<number>;
  @Output() public lock: EventEmitter<number>;
  @Output() public unlock: EventEmitter<number>;
  @Output() public enableEnrollee: EventEmitter<number>;
  @Output() public toggleManualAdj: EventEmitter<{ enrolleeId: number, alwaysManual: boolean }>;
  @Output() public enableEditing: EventEmitter<number>;
  @Output() public rerunRules: EventEmitter<number>;
  @Output() public delete: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;
  @Output() public assign: EventEmitter<{ enrolleeId: number, agreementType: AgreementType }>;
  public form: FormGroup;
  public termsOfAccessAgreements: { type: AgreementType, name: string }[];

  public EnrolmentStatus = EnrolmentStatus;
  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private authService: AuthService,
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
    this.rerunRules = new EventEmitter<number>();
    this.delete = new EventEmitter<number>();
    this.assign = new EventEmitter<{ enrolleeId: number, agreementType: AgreementType }>();
    this.toggleManualAdj = new EventEmitter<{ enrolleeId: number, alwaysManual: boolean }>();
    this.route = new EventEmitter<string | (string | number)[]>();

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

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public get canDelete(): boolean {
    return this.authService.isSuperAdmin();
  }

  public get isUnderReview(): boolean {
    return (this.enrollee && this.enrollee.currentStatusCode === EnrolmentStatus.UNDER_REVIEW);
  }

  public onApprove() {
    if (this.formUtilsService.checkValidity(this.form) && this.canEdit && this.isUnderReview) {
      const agreementName = this.termsOfAccessAgreements
        .filter(t => t.type === this.assignedTOAType.value)[0]
        .name;
      this.approve.emit({ enrolleeId: this.enrollee.id, agreementName });
    }
  }

  public onDecline() {
    if (this.canEdit && this.isUnderReview) {
      this.decline.emit(this.enrollee.id);
    }
  }

  public onLock() {
    if (this.canEdit) {
      this.lock.emit(this.enrollee.id);
    }
  }

  public onUnlock() {
    if (this.canEdit) {
      this.unlock.emit(this.enrollee.id);
    }
  }

  public onDelete() {
    if (this.canDelete) {
      this.delete.emit(this.enrollee.id);
    }
  }

  public onEnableEnrollee() {
    if (this.canEdit) {
      this.enableEnrollee.emit(this.enrollee.id);
    }
  }

  public onEnableEditing() {
    if (this.canEdit) {
      this.enableEditing.emit(this.enrollee.id);
    }
  }

  public onRerunRules() {
    if (this.canEdit) {
      this.rerunRules.emit(this.enrollee.id);
    }
  }

  public onToggleManualAdj() {
    if (this.canEdit) {
      this.toggleManualAdj.emit({
        enrolleeId: this.enrollee.id,
        alwaysManual: !this.enrollee.alwaysManual
      });
    }
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
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
        this.assign.emit({ enrolleeId: this.enrollee.id, agreementType })
      );
  }
}
