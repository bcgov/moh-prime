import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-adjudicator-actions',
  templateUrl: './adjudicator-actions.component.html',
  styleUrls: ['./adjudicator-actions.component.scss']
})
export class AdjudicatorActionsComponent implements OnInit {
  @Input() public enrollee: HttpEnrollee;
  @Output() public approve: EventEmitter<HttpEnrollee>;
  @Output() public decline: EventEmitter<number>;
  @Output() public unlock: EventEmitter<number>;
  @Output() public delete: EventEmitter<number>;
  @Output() public route: EventEmitter<string | (string | number)[]>;

  public EnrolmentStatus = EnrolmentStatus;
  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private authService: AuthService
  ) {
    this.approve = new EventEmitter<HttpEnrollee>();
    this.decline = new EventEmitter<number>();
    this.unlock = new EventEmitter<number>();
    this.delete = new EventEmitter<number>();
    this.route = new EventEmitter<string | (string | number)[]>();
  }

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public get canDelete(): boolean {
    return this.authService.isSuperAdmin();
  }

  public get isUnderReview(): boolean {
    return (this.enrollee && this.enrollee.currentStatus.statusCode === EnrolmentStatus.UNDER_REVIEW);
  }

  public onApprove() {
    // TODO might be better to pass the enrolle ID and find it on the other side
    if (this.canEdit && this.isUnderReview) {
      this.approve.emit(this.enrollee);
    }
  }

  public onDecline() {
    if (this.canEdit && this.isUnderReview) {
      this.decline.emit(this.enrollee.id);
    }
  }

  public onUnlock() {
    if (this.canEdit && this.isUnderReview) {
      this.unlock.emit(this.enrollee.id);
    }
  }

  public onDelete() {
    if (this.canDelete) {
      this.delete.emit(this.enrollee.id);
    }
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.route.emit(routePath);
  }

  public ngOnInit() { }
}
