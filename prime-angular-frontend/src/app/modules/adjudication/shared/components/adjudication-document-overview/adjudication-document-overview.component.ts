import { Component, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentAgreementTypeNameMap } from '@shared/enums/agreement-type.enum';

@Component({
  selector: 'app-adjudication-document-overview',
  template: `
    <app-page-section>
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Submitted Documents</ng-container>
      </app-page-subheader>

      <app-enrollee-property *ngIf="documents?.length"
                            title="Filename">
        <ng-container *ngFor="let document of documents">
          <button mat-stroked-button
                  color="primary"
                  class="mb-2"
                  (click)="downloadDocument(document.id)">
            <mat-icon class="mr-2">attachment</mat-icon>
            {{ document.filename | default }}
          </button>
          <br>
        </ng-container>
      </app-enrollee-property>
    </app-page-section>
  `,
  styles: [
    '.mat-icon { font-size: 1.2em; }',
    '.button > .mat-icon { font-size: 1.35rem; }'
  ],
})
export class AdjudicationDocumentOverviewComponent extends AbstractOverview {
  @Input() public documents: BaseDocument[];
  @Output() public download: EventEmitter<{ documentId: number }>;
  public PaperEnrolmentAgreementTypeNameMap = PaperEnrolmentAgreementTypeNameMap;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, PaperEnrolmentRoutes.MODULE_PATH);

    this.download = new EventEmitter<{ documentId: number }>();
  }

  public downloadDocument(documentId: number): void {
    this.download.emit({ documentId });
  }

}
