import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentAgreementTypeNameMap } from '@shared/enums/agreement-type.enum';

@Component({
  selector: 'app-adjudication-document-overview',
  templateUrl: './adjudication-document-overview.component.html',
  styles: [
    '.mat-icon { font-size: 1.2em; }',
    '.button > .mat-icon { font-size: 1.35rem; }'
  ],
})
export class AdjudicationDocumentOverviewComponent extends AbstractOverview {
  @Input() public documents: BaseDocument[];
  @Output() public download: EventEmitter<{ documentId: number }>;
  public PaperEnrolmentAgreementTypeNameMap = PaperEnrolmentAgreementTypeNameMap;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;
  public TOADocuments: BaseDocument[];
  public supportingDocuments: BaseDocument[];
  public paperForms: BaseDocument[];

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, PaperEnrolmentRoutes.MODULE_PATH);

    this.download = new EventEmitter<{ documentId: number }>();
    this.TOADocuments = [];
    this.supportingDocuments = [];
    this.paperForms = [];
  }

  ngOnInit() {
    if (this.documents) {
      this.documents.forEach((document) => {
        switch(document.documentType) {
          case (1): {
            this.TOADocuments.push(document);
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

  public downloadDocument(documentId: number): void {
    this.download.emit({ documentId });
  }

}
