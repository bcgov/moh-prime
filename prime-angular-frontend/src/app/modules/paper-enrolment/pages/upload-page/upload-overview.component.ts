import { ChangeDetectionStrategy, Component, EventEmitter, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { AgreementTypeNameMap } from '@shared/enums/agreement-type.enum';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { UploadForm } from './upload-form.model';

@Component({
  selector: 'app-upload-overview',
  template: `
    <app-page-section>
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Job Site Information</ng-container>

        <button *ngIf="true"
                mat-icon-button
                matTooltip="Edit Upload Information"
                (click)="onRoute(PaperEnrolmentRoutes.UPLOAD)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-page-subheader>
      <app-enrollee-property title="TOA Type">
        {{ AgreementTypeNameMap[upload?.assignedTOAType] }}
      </app-enrollee-property>

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
        </ng-container>
      </app-enrollee-property>
    </app-page-section>
  `,
  styles: [
    '.mat-icon { font-size: 1.2em; }',
    '.button > .mat-icon { font-size: 1.35rem; }'
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UploadOverviewComponent extends AbstractOverview {
  @Input() public upload: UploadForm;
  @Input() public documents: BaseDocument[];
  @Output() public download: EventEmitter<{ documentId: number }>;
  public AgreementTypeNameMap = AgreementTypeNameMap;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;

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
