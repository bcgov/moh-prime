import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY, Subscription } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-overview-page',
  templateUrl: './overview-page.component.html',
  styleUrls: ['./overview-page.component.scss']
})
export class OverviewPageComponent implements OnInit {
  public busy: Subscription;
  public enrollee: HttpEnrollee;
  public routeUtils: RouteUtils;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;

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
      message: 'Are you ready to submit this enrolment?',
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

  public onRoute(routePath: string | string[]) {
    routePath = (Array.isArray(routePath)) ? routePath : [routePath];
    this.routeUtils.routeRelativeTo(routePath);
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo([PaperEnrolmentRoutes.UPLOAD]);
  }

  public ngOnInit(): void {
    this.paperEnrolmentResource.getEnrolleeById(+this.route.snapshot.params.eid)
      .subscribe((enrollee: HttpEnrollee) => this.enrollee = enrollee);
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo([PaperEnrolmentRoutes.NEXT_STEPS]);
  }
}
