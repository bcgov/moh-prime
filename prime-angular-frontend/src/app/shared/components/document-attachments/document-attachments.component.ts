import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RouteUtils } from '@lib/utils/route-utils.class';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { DocumentSectionMap } from '@shared/enums/document-type.enum';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';

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

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, PaperEnrolmentRoutes.MODULE_PATH);

    this.documents = [];
    this.documentsGroupedByType = {};
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public ngOnInit(): void {
    if (this.documents) {
      this.documents.forEach((document) => {
        if (!this.documentsGroupedByType[document.documentType]) {
          this.documentsGroupedByType[document.documentType] = new Array<BaseDocument>();
        }
        this.documentsGroupedByType[document.documentType].push(document);
      });
    }
  }
}
