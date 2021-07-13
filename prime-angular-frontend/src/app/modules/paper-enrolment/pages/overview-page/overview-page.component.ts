import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY, forkJoin, pipe, Subscription } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { UtilsService } from '@core/services/utils.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { EnrolleeAdjudicationDocument } from '@registration/shared/models/adjudication-document.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/shared/services/paper-enrolment-resource.service';

@Component({
  selector: 'app-overview-page',
  templateUrl: './overview-page.component.html',
  styleUrls: ['./overview-page.component.scss']
})
export class OverviewPageComponent implements OnInit {
  public busy: Subscription;
  public enrollee: HttpEnrollee;
  public routeUtils: RouteUtils;
  public documents: EnrolleeAdjudicationDocument[];
  public CareSettingEnum = CareSettingEnum;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private route: ActivatedRoute,
    private utilsService: UtilsService,
    router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public onDownload({ documentId }: { documentId: number }): void {
    const enrolleeId = +this.route.snapshot.params.eid;
    this.paperEnrolmentResource.getEnrolleeAdjudicationDocumentDownloadToken(enrolleeId, documentId)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
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
    this.routeUtils.routeRelativeTo(routePath);
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(PaperEnrolmentRoutes.UPLOAD);
  }

  public ngOnInit(): void {
    this.paperEnrolmentResource.getEnrolleeById(+this.route.snapshot.params.eid)
      .subscribe((enrollee: HttpEnrollee) => this.enrollee = enrollee);

    this.paperEnrolmentResource.getAdjudicationDocuments(+this.route.snapshot.params.eid)
      .subscribe((documents: EnrolleeAdjudicationDocument[]) => this.documents = documents);
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo(PaperEnrolmentRoutes.NEXT_STEPS);
  }
}
