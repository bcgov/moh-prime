import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationExtras } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY, Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { HttpEnrollee } from '@shared/models/enrolment.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

@Component({
  selector: 'app-overview-page',
  templateUrl: './overview-page.component.html',
  styleUrls: ['./overview-page.component.scss']
})
export class OverviewPageComponent implements OnInit {
  public busy: Subscription;
  public enrollee: HttpEnrollee;
  public EnrolmentStatus = EnrolmentStatus;
  public routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public onSubmit() {
    const data: DialogOptions = {
      title: 'Submit Enrolment',
      message: 'When your enrolment is submitted for adjudication, it can no longer be updated. Are you ready to submit your enrolment?',
      actionText: 'Submit Enrolment'
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? this.paperEnrolmentResource.finalize(+this.route.snapshot.params.eid)
            : EMPTY
        )
      )
      .subscribe(() => this.afterSubmitIsSuccessful());
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo([PaperEnrolmentRoutes.SELF_DECLARATION]);
  }

  public ngOnInit(): void {
    this.paperEnrolmentResource.getEnrolleeById(+this.route.snapshot.params.eid)
      .subscribe((enrollee: HttpEnrollee) => this.enrollee = enrollee);
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo(['./', PaperEnrolmentRoutes.OVERVIEW]);
  }
}
