import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription, EMPTY, noop, of } from 'rxjs';

import { HttpEnrollee } from '@shared/models/enrolment.model';
import { AbstractComponent } from '@shared/classes/abstract-component';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { exhaustMap } from 'rxjs/operators';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { ApproveEnrolmentComponent } from '@shared/components/dialogs/content/approve-enrolment/approve-enrolment.component';
import { MatDialog } from '@angular/material';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-enrollee-events',
  templateUrl: './enrollee-events.component.html',
  styleUrls: ['./enrollee-events.component.scss']
})
export class EnrolleeEventsComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  public enrollee: HttpEnrollee;
  public hasActions: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private authService: AuthService,
    private adjudicationResource: AdjudicationResource,
    private dialog: MatDialog
  ) {
    super(route, router);

    this.baseRoutePath = [AdjudicationRoutes.MODULE_PATH, AdjudicationRoutes.ENROLLEES];
    this.hasActions = true;
  }

  public onClaim(enrolleeId: number) {
    this.adjudicationResource
      .setEnrolleeAdjudicator(enrolleeId)
      .subscribe((updatedEnrollee: HttpEnrollee) => this.enrollee = updatedEnrollee);
  }

  public onDisclaim(enrolleeId: number) {
    this.adjudicationResource
      .removeEnrolleeAdjudicator(enrolleeId)
      .subscribe((updatedEnrollee: HttpEnrollee) => this.enrollee = updatedEnrollee);
  }

  public onApprove(enrollee: HttpEnrollee) {
    const data: DialogOptions = {
      title: 'Approve Enrolment',
      message: 'Are you sure you want to approve this enrolment?',
      actionText: 'Approve Enrolment',
      data: { enrollee },
      component: ApproveEnrolmentComponent
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: boolean }) => {
          if (result) {
            (result.output)
              ? this.adjudicationResource.updateEnrolleeAlwaysManual(enrollee.id, result.output)
              : of(noop);
          }

          return EMPTY;
        }),
        exhaustMap(() =>
          this.adjudicationResource.createEnrolmentStatus(enrollee.id, EnrolmentStatus.REQUIRES_TOA)
        ),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrollee.id))
      )
      .subscribe((approvedEnrollee: HttpEnrollee) => this.enrollee = approvedEnrollee);
  }

  public onDecline(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Decline Enrolment',
      message: 'Are you sure you want to decline this enrolment?',
      actionType: 'warn',
      actionText: 'Decline Enrolment'
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.adjudicationResource.createEnrolmentStatus(enrolleeId, EnrolmentStatus.LOCKED)
            : EMPTY
        ),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrolleeId)),
      )
      .subscribe((declinedEnrollee: HttpEnrollee) => this.enrollee = declinedEnrollee);
  }

  public onUnlock(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Unlock for Editing',
      message: 'When unlocked the enrollee will be able to edit and update their enrolment. Are you sure you want to unlock this enrolment?',
      actionType: 'warn',
      actionText: 'Unlock for Editing'
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.adjudicationResource.createEnrolmentStatus(enrolleeId, EnrolmentStatus.ACTIVE)
            : EMPTY
        ),
        exhaustMap(() => this.adjudicationResource.getEnrolleeById(enrolleeId))
      )
      .subscribe((lockedEnrollee: HttpEnrollee) => this.enrollee = lockedEnrollee);
  }

  public onDelete(enrolleeId: number) {
    const data: DialogOptions = {
      title: 'Delete Enrolment',
      message: 'Are you sure you want to delete this enrolment?',
      actionType: 'warn',
      actionText: 'Delete Enrolment'
    };

    if (this.authService.isSuperAdmin()) {
      // TODO if they delete the enrollee they are on need to route
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? this.adjudicationResource.deleteEnrollee(enrolleeId)
              : EMPTY
          )
        )
        .subscribe((enrollee: HttpEnrollee) => this.routeTo(this.baseRoutePath));
    }
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeWithin(routePath);
  }

  public ngOnInit() {
    this.getEnrolleeById();
  }

  private getEnrolleeById() {
    const enrolleeId = this.route.snapshot.params.id;
    this.busy = this.adjudicationResource
      .getEnrolleeById(enrolleeId)
      .subscribe((enrollee: HttpEnrollee) => this.enrollee = enrollee);
  }
}
