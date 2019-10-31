import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

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
  selector: 'app-access-agreement',
  templateUrl: './access-agreement.component.html',
  styleUrls: ['./access-agreement.component.scss']
})
export class AccessAgreementComponent implements OnInit {
  public enrolment: Enrolment;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
    private toastService: ToastService,
    private dialog: MatDialog,
    private logger: LoggerService
  ) { }

  ngOnInit() {
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

  public onSubmit() {
    const enrolment = this.enrolmentStateService.enrolment;
    const data: DialogOptions = {
      title: 'Access Agreement',
      message: 'Are you sure you want to accept the access agreement?',
      actionText: 'Accept Agreement'
    };
    this.dialog.open(ConfirmDialogComponent, { data })
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
          this.toastService.openSuccessToast('Enrolment has been submitted');
          this.router.navigate(['confirmation'], { relativeTo: this.route.parent });
        },
        (error: any) => {
          this.toastService.openErrorToast('Enrolment could not be submitted');
          this.logger.error('[Enrolment] Review::onSubmit error has occurred: ', error);
        }
      );
  }

}
