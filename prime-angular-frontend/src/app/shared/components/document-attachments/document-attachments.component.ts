import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { UtilsService } from '@core/services/utils.service';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { DocumentSectionMap } from '@shared/enums/document-type.enum';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';

import { PaperEnrolmentResource } from '@paper-enrolment/shared/services/paper-enrolment-resource.service';


@Component({
  selector: 'app-document-attachments',
  templateUrl: './document-attachments.component.html',
  styleUrls: ['./document-attachments.component.scss']
})
export class DocumentAttachmentsComponent extends AbstractOverview implements OnInit {
  @Input() public documents: BaseDocument[];
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;
  public documentsGroupedByType: { [key: number]: BaseDocument[] };
  public DocumentSectionMap = DocumentSectionMap;
  public route: ActivatedRoute;
  public router: Router;

  constructor(
    route: ActivatedRoute,
    router: Router,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private utilsService: UtilsService
  ) {
    super(route, router, PaperEnrolmentRoutes.MODULE_PATH);

    this.documentsGroupedByType = {};
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public onDownload({ documentId }: { documentId: number }): void {
    const enrolleeId = +this.route.snapshot.params.id
      ? this.route.snapshot.params.id
      : this.route.snapshot.params.eid;
    this.paperEnrolmentResource.getEnrolleeAdjudicationDocumentDownloadToken(enrolleeId, documentId)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }

  public ngOnInit(): void {
    this.documents.forEach((document) => {
      if (!this.documentsGroupedByType[document.documentType]) {
        this.documentsGroupedByType[document.documentType] = new Array<BaseDocument>();
      }
      this.documentsGroupedByType[document.documentType].push(document);
    });
  }
}
