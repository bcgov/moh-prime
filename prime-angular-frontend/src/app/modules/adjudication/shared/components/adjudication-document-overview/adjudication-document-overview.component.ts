import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentAgreementTypeNameMap } from '@shared/enums/agreement-type.enum';

@Component({
  selector: 'app-adjudication-document-overview',
  templateUrl: './adjudication-document-overview.component.html',
  styleUrls: ['./adjudication-document-overview.component.scss']
})
export class AdjudicationDocumentOverviewComponent extends AbstractOverview implements OnInit {
  @Input() public documents: BaseDocument[];
  @Output() public download: EventEmitter<{ documentId: number }>;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;
  public ToaDocuments: BaseDocument[];
  public supportingDocuments: BaseDocument[];
  public paperForms: BaseDocument[];

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, PaperEnrolmentRoutes.MODULE_PATH);

    this.download = new EventEmitter<{ documentId: number }>();
    this.ToaDocuments = [];
    this.supportingDocuments = [];
    this.paperForms = [];
  }

  public onDownload(documentId: number): void {
    this.download.emit({ documentId });
  }

  ngOnInit(): void {
    if (this.documents) {
      this.documents.forEach((document) => {
        switch (document.documentType) {
          case (1): {
            this.ToaDocuments.push(document);
            break;
          }
          case (2): {
            this.supportingDocuments.push(document);
            break;
          }
          case (3): {
            this.paperForms.push(document);
            break;
          }
        }
      });
    }
  }
}
