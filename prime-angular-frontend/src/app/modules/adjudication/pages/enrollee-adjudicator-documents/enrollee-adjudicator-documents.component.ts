import { AdjudicatorDocumentsComponent } from '@adjudication/shared/components/adjudicator-documents/adjudicator-documents.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { forkJoin, Observable, Subscription } from 'rxjs';

import { UtilsService } from '@core/services/utils.service';
import { ToastService } from '@core/services/toast.service';

import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolleeAdjudicationDocument } from '@registration/shared/models/adjudication-document.model';

@Component({
  selector: 'app-enrollee-adjudicator-documents',
  templateUrl: './enrollee-adjudicator-documents.component.html',
  styleUrls: ['./enrollee-adjudicator-documents.component.scss']
})
export class EnrolleeAdjudicatorDocumentsComponent implements OnInit {
  public documents$: Observable<EnrolleeAdjudicationDocument[]>;
  public busy: Subscription;
  private enrolleeId: number;
  @ViewChild('adjudicationDocuments') public adjudicatorDocumentsComponent: AdjudicatorDocumentsComponent;

  constructor(
    private enrolmentResource: EnrolmentResource,
    private route: ActivatedRoute,
    private utilsService: UtilsService,
    private toastService: ToastService
  ) {
    this.enrolleeId = this.route.snapshot.params.id;
  }

  public onSaveDocuments(documentGuids: string[]) {
    const documentGuids$ = documentGuids.map(guid =>
      this.enrolmentResource.createEnrolleeAdjudicationDocument(this.enrolleeId, guid));

    forkJoin(documentGuids$)
      .subscribe(val => {
        this.getDocuments();
        this.adjudicatorDocumentsComponent.removeFiles();
      });
  }

  public onGetDocumentByGuid(documentId: number) {
    this.enrolmentResource.getEnrolleeAdjudicationDocumentDownloadToken(this.enrolleeId, documentId)
      .subscribe((token: string) =>
        this.utilsService.downloadToken(token)
      );
  }

  public onDeleteDocumentById(documentId: number) {
    this.busy = this.enrolmentResource.deleteEnrolleeAdjudicationDocument(this.enrolleeId, documentId)
      .subscribe((document: EnrolleeAdjudicationDocument) => {
        this.getDocuments();
        this.toastService.openSuccessToast('Document has been deleted');
      });
  }

  ngOnInit(): void {
    this.getDocuments();
  }

  private getDocuments() {
    this.documents$ = this.enrolmentResource
      .getEnrolleeAdjudicationDocuments(this.enrolleeId);
  }
}
