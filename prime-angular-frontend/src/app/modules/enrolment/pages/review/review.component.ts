import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog, MatCheckboxChange } from '@angular/material';

import { map, exhaustMap } from 'rxjs/operators';
import { EMPTY } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss']
})
export class ReviewComponent implements OnInit {
  // TODO: make a proper enrolment model
  public enrolment: Enrolment;
  disabledAgreement = true;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
    private toastService: ToastService,
    private dialog: MatDialog,
    private logger: LoggerService
  ) { }

  public onSubmit() {
    if (this.enrolmentStateService.isEnrolmentValid()) {
      const enrolment = this.enrolmentStateService.enrolment;
      const data: DialogOptions = {
        title: 'Submit Enrolment',
        message: 'When your enrolment has submitted for adjudication it can no longer be updated. Are you ready to submit your enrolment?',
        actionText: 'Submit Enrolment'
      };
      this.dialog.open(ConfirmDialogComponent, { data })
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
            this.router.navigate(['confirmation'], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('Enrolment could not be submitted');
            this.logger.error('[Enrolment] Review::onSubmit error has occurred: ', error);
          });
    } else {
      // TODO: indicate where validation failed in the review to prompt user edits
      console.log('PROFILE', this.enrolmentStateService.isProfileInfoValid());
      console.log('REGULATORY', this.enrolmentStateService.isRegulatoryValid());
      console.log('DEVICE_PROVIDER', this.enrolmentStateService.isDeviceProviderValid());
      console.log('JOBS', this.enrolmentStateService.isJobsValid());
      console.log('PROFESSIONAL', this.enrolmentStateService.isProfessionalInfoValid());
      console.log('DECLARATION', this.enrolmentStateService.isSelfDeclarationValid());
      console.log('ACCESS', this.enrolmentStateService.isPharmaNetAccessValid());
    }
  }

  public onConfirmAccuracy(event: MatCheckboxChange) {
    this.disabledAgreement = !event.checked;
  }

  public showYesNo(declared: boolean) {
    return (declared === null) ? 'N/A'
      : (declared) ? 'Yes' : 'No';
  }

  public redirect(route: string) {
    this.router.navigate(['enrolment', route]);
  }

  public ngOnInit() {
    // TODO: detect enrolment already exists and don't reload
    // TODO: apply guard if no enrolment is found to redirect to profile
    this.enrolmentResource.enrolments()
      .pipe(
        map((enrolment: Enrolment) => this.enrolment = enrolment)
      )
      .subscribe((enrolment: Enrolment) => {
        if (enrolment) {
          this.enrolmentStateService.enrolment = enrolment;
        }
      });
  }
}
