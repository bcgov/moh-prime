import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { exhaustMap } from 'rxjs/operators';
import { EMPTY, Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent extends BaseEnrolmentPage implements OnInit {
  public busy: Subscription;
  public enrolment: Enrolment;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private dialog: MatDialog,
    private enrolmentService: EnrolmentService,
    private enrolmentResource: EnrolmentResource,
    private enrolmentStateService: EnrolmentStateService,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    super(route, router);
  }

  public onSubmit() {
    if (this.enrolmentStateService.isEnrolmentValid()) {
      const enrolment = this.enrolmentStateService.enrolment;
      const data: DialogOptions = {
        title: 'Submit Enrolment',
        message: 'When your enrolment is submitted for adjudication it can no longer be updated. Are you ready to submit your enrolment?',
        actionText: 'Submit Enrolment'
      };
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? this.enrolmentResource.updateEnrolmentStatus(enrolment.id, EnrolmentStatus.SUBMITTED)
              : EMPTY
          )
        )
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Enrolment has been submitted');
            this.router.navigate([EnrolmentRoutes.SUBMISSION_CONFIRMATION], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('Enrolment could not be submitted');
            this.logger.error('[Enrolment] Review::onSubmit error has occurred: ', error);
          });
    } else {
      this.toastService.openErrorToast('Your enrolment has an error that needs to be corrected before you will be able to submit');

      console.log('DEMOGRAPHIC', this.enrolmentStateService.isProfileInfoValid());
      console.log('REGULATORY', this.enrolmentStateService.isRegulatoryValid());
      console.log('JOBS', this.enrolmentStateService.isJobsValid());
      console.log('ORGANIZATION', this.enrolmentStateService.isOrganizationValid());
      console.log('SELF DECLARATION', this.enrolmentStateService.isSelfDeclarationValid());
    }
  }

  public ngOnInit() {
    let enrolment = this.enrolmentService.enrolment;
    if (this.enrolmentStateService.isPatched) {
      enrolment = this.enrolmentStateService.enrolment;
      // Merge BCSC information in for use within the view
      const {
        firstName,
        middleName,
        lastName,
        dateOfBirth,
        physicalAddress
      } = this.enrolmentService.enrolment.enrollee;
      enrolment.enrollee = { ...enrolment.enrollee, firstName, middleName, lastName, dateOfBirth, physicalAddress };
    }

    // Store a local copy of the enrolment for views
    this.enrolment = enrolment;
    this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment;

    // Attempt to patch the form if not already patched
    this.enrolmentStateService.setEnrolment(enrolment);
  }
}
