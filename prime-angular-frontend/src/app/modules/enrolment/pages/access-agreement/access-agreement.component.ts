import { Component, OnInit, ViewChild, TemplateRef, AfterViewInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog, MatCheckboxChange } from '@angular/material';

import { map, exhaustMap } from 'rxjs/operators';
import { EMPTY, Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentRoutes } from '@enrolment/enrolent.routes';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-access-agreement',
  templateUrl: './access-agreement.component.html',
  styleUrls: ['./access-agreement.component.scss']
})
// TODO hacked to align last minute with demo expectations :(
export class AccessAgreementComponent implements OnInit {
  public busy: Subscription;
  public enrolment: Enrolment;
  public currentPageNumber: number;
  public disabledAgreement: boolean;
  public hasAcceptedEachPage: boolean;
  public totalPages: number;
  public acceptCtrl: FormControl;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
    private toastService: ToastService,
    private dialog: MatDialog,
    private logger: LoggerService
  ) {
    this.currentPageNumber = 0;
    this.disabledAgreement = true;
    this.hasAcceptedEachPage = false;
  }

  public get isMOA(): boolean {
    return true;
  }

  public onSubmit() {
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

  public onPrint() {
    this.toastService.openSuccessToast('Acceptance agreement is being prepared for downloading and printing')
  }

  public onCheckAcceptance(event: MatCheckboxChange) {
    this.disabledAgreement = !event.checked;
  }

  public onAcceptPage() {
    this.currentPageNumber++;

    if (this.currentPageNumber === 13) {
      // Show accept agreement instead of continue
      this.hasAcceptedEachPage = true;
    } else {
      // Disable continue and reset acceptance check
      this.disabledAgreement = true;
      this.acceptCtrl.reset();
    }
  }

  public ngOnInit() {
    this.busy = this.enrolmentResource.enrolments()
      .pipe(
        map((enrolment: Enrolment) => this.enrolment = enrolment)
      )
      .subscribe((enrolment: Enrolment) => {
        if (enrolment) {
          this.enrolmentStateService.enrolment = enrolment;
          this.totalPages = 13; // MOA/OBO
        }
      });

    this.acceptCtrl = new FormControl();
  }
}
