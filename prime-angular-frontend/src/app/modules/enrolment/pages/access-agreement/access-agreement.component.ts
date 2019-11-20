import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog, MatCheckboxChange } from '@angular/material';
import { FormControl } from '@angular/forms';

import { map, exhaustMap } from 'rxjs/operators';
import { EMPTY, Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

@Component({
  selector: 'app-access-agreement',
  templateUrl: './access-agreement.component.html',
  styleUrls: ['./access-agreement.component.scss']
})
export class AccessAgreementComponent implements OnInit {
  public busy: Subscription;
  public enrolment: Enrolment;
  public currentPage: number;
  public agree: FormControl;
  public disabled: boolean;
  public hasReadAgreement: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
    private toastService: ToastService,
    private dialog: MatDialog,
    private changeDetectorRef: ChangeDetectorRef,
    private utilsService: UtilsService,
    private logger: LoggerService
  ) {
    this.currentPage = 0;
    this.agree = new FormControl(false);
    this.disabled = true;
    this.hasReadAgreement = false;
  }

  public onSubmit() {
    if (this.hasReadAgreement) {
      const enrolment = this.enrolmentStateService.enrolment;
      const data: DialogOptions = {
        title: 'Access Agreement',
        message: 'Are you sure you want to accept the access agreement?',
        actionText: 'Accept Agreement'
      };
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? this.enrolmentResource.updateEnrolmentStatus(enrolment.id, EnrolmentStatus.ACCEPTED_TOS)
              : EMPTY
          )
        )
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Access agreement has been accepted');
            this.router.navigate([EnrolmentRoutes.SUMMARY], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('Access agreement could not be accepted');
            this.logger.error('[Enrolment] AccessAgreement::onSubmit error has occurred: ', error);
          }
        );
    }
  }

  public onPrevPage() {
    if (this.currentPage > 0) {
      this.agree.reset();
      this.disabled = true;
      this.utilsService.scrollTop();
      this.currentPage--;
      this.hasReadAgreement = false;
    }
  }

  public onNextPage() {
    if (!this.hasReadAgreement) {
      this.utilsService.scrollTop();
      this.currentPage++;
    }
  }

  public onConfirmAgreement(value: MatCheckboxChange) {
    this.disabled = !value.checked;
  }

  public onPageChange(agreement: { atEnd: boolean }) {
    if (agreement.atEnd) {
      this.hasReadAgreement = agreement.atEnd;
      this.changeDetectorRef.detectChanges();
    }
  }

  public ngOnInit() {
    // TODO drop and access using enrolment service
    this.busy = this.enrolmentResource.enrolments()
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
