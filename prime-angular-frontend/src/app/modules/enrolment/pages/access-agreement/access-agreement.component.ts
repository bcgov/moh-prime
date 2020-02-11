import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';
import { FormControl } from '@angular/forms';

import { exhaustMap } from 'rxjs/operators';
import { EMPTY } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { AccessTerm } from '@enrolment/shared/models/access-term.model';
import { ViewportService } from '@core/services/viewport.service';

@Component({
  selector: 'app-access-agreement',
  templateUrl: './access-agreement.component.html',
  styleUrls: ['./access-agreement.component.scss']
})
export class AccessAgreementComponent extends BaseEnrolmentPage implements OnInit {
  public enrolment: Enrolment;
  public isAutomatic: boolean;
  public currentPage: number;
  public hasReadAgreement: boolean;
  public agreed: FormControl;

  // Allow the use of enum in the component template
  public EnrolmentStatus = EnrolmentStatus;
  public EnrolleeClassification = EnrolleeClassification;

  public accessTerm: AccessTerm;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private dialog: MatDialog,
    private changeDetectorRef: ChangeDetectorRef,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private toastService: ToastService,
    private utilsService: UtilsService,
    private viewportService: ViewportService,
    private logger: LoggerService
  ) {
    super(route, router);

    this.currentPage = 0;
    this.hasReadAgreement = false;
    this.agreed = new FormControl(false);
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public get isObo() {
    return this.accessTerm.userClause.enrolleeClassification === EnrolleeClassification.OBO;
  }

  public get isRu() {
    return this.accessTerm.userClause.enrolleeClassification === EnrolleeClassification.RU;
  }

  public get hasAgreed(): boolean {
    return this.agreed.value;
  }

  public onSubmit(enrolmentStatus: EnrolmentStatus) {
    if (this.hasReadAgreement) {
      const status = (enrolmentStatus === EnrolmentStatus.ACCEPTED_TOS)
        ? { verb: 'Accept', adjective: 'accepted' }
        : { verb: 'Decline', adjective: 'declined' };

      const data: DialogOptions = {
        title: 'Terms of Access',
        message: `Are you sure you want to ${status.verb.toLowerCase()} the terms of access?`,
        actionText: `${status.verb} Agreement`
      };
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? this.enrolmentResource.updateEnrolmentStatus(this.enrolment.id, enrolmentStatus)
              : EMPTY
          )
        )
        .subscribe(
          () => {
            this.toastService.openSuccessToast(`Terms of Access has been ${status.adjective}`);
            this.routeTo(EnrolmentRoutes.PHARMANET_ENROLMENT_CERTIFICATE, {
              state: { showProgressBar: this.isInitialEnrolment }
            });
          },
          (error: any) => {
            this.toastService.openErrorToast(`Terms of Access could not be ${status.adjective}`);
            this.logger.error('[Enrolment] AccessAgreement::onSubmit error has occurred: ', error);
          }
        );
    }
  }

  public onAcceptedAgreement() {
    const data: DialogOptions = {
      title: 'Accept Terms of Access',
      message: 'Are you sure you want to accept the terms of access?',
      actionText: 'Accept Agreement'
    };
  }

  public onDeclinedAgreement() {
    const data: DialogOptions = {
      title: 'Decline Terms of Access',
      message: 'Are you sure you want to decline the terms of access?',
      actionText: 'Decline Agreement'
    };
  }

  public onPrevPage() {
    if (this.currentPage > 0) {
      this.utilsService.scrollTop();
      this.currentPage--;
      this.hasReadAgreement = false;
      this.agreed.reset(false);
    }
  }

  public onNextPage() {
    if (!this.hasReadAgreement) {
      this.utilsService.scrollTop();
      this.currentPage++;

      this.onPageChange({ atEnd: true });
    }
  }

  public onPageChange(agreement: { atEnd: boolean }) {
    if (agreement.atEnd) {
      this.hasReadAgreement = agreement.atEnd;
      this.changeDetectorRef.detectChanges();
    }
  }

  public ngOnInit() {
    this.enrolment = this.enrolmentService.enrolment;
    this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment;
    this.enrolmentResource.getAccessTermLatest(this.enrolment.id, false)
      .subscribe(
        (accessTerm: AccessTerm) => this.accessTerm = accessTerm,
        (error: any) => {
          this.toastService.openErrorToast(`Terms of access could not be found`);
          this.logger.error('[Enrolment] AccessAgreement::ngOnInit error has occurred: ', error);
        }
      );
  }
}
