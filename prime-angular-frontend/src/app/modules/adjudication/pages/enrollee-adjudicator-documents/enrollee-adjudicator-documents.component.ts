import { AdjudicatorDocumentsComponent } from '@adjudication/shared/components/adjudicator-documents/adjudicator-documents.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { forkJoin, Observable, Subscription } from 'rxjs';

import { UtilsService } from '@core/services/utils.service';

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
  @ViewChild('adjudicationDocuments') public adjudicatorDocumentsComponent: AdjudicatorDocumentsComponent;

  constructor(
    private enrolmentResource: EnrolmentResource,
    private route: ActivatedRoute,
    private utilsService: UtilsService
  ) { }

  public onSaveDocuments(documentGuids: string[]): void {
    const enrolleeId = +this.route.snapshot.params.id;
    const documentGuids$ = documentGuids.map(guid =>
      this.enrolmentResource.createEnrolleeAdjudicationDocument(enrolleeId, guid));

    forkJoin(documentGuids$)
      .subscribe(val => {
        this.getDocuments(enrolleeId);
        this.adjudicatorDocumentsComponent.removeFiles();
      });
  }

  public onGetDocumentByGuid(documentId: number): void {
    this.enrolmentResource.getEnrolleeAdjudicationDocumentDownloadToken(+this.route.snapshot.params.id, documentId)
      .subscribe((token: string) =>
        this.utilsService.downloadToken(token)
      );
  }

  public onDeleteDocumentById(documentId: number): void {
    const enrolleeId = +this.route.snapshot.params.id;
    this.busy = this.enrolmentResource.deleteEnrolleeAdjudicationDocument(enrolleeId, documentId)
      .subscribe((document: EnrolleeAdjudicationDocument) => this.getDocuments(enrolleeId));
  }

  ngOnInit(): void {
    this.getDocuments(+this.route.snapshot.params.id);
    this.route.params.subscribe(params => this.getDocuments(+params.id));
  }

  private getDocuments(enrolleeId: number): void {
    this.documents$ = this.enrolmentResource
      .getEnrolleeAdjudicationDocuments(enrolleeId);
  }
}
