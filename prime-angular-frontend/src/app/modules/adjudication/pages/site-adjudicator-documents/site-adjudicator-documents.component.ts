import { AdjudicatorDocumentsComponent } from '@adjudication/shared/components/adjudicator-documents/adjudicator-documents.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { forkJoin, Observable, of, Subscription } from 'rxjs';

import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteAdjudicationDocument } from '@registration/shared/models/adjudication-document.model';

@Component({
  selector: 'app-site-adjudicator-documents',
  templateUrl: './site-adjudicator-documents.component.html',
  styleUrls: ['./site-adjudicator-documents.component.scss']
})
export class SiteAdjudicatorDocumentsComponent implements OnInit {
  public documents$: Observable<SiteAdjudicationDocument[]>;
  public busy: Subscription;
  @ViewChild('adjudicationDocuments') public adjudicatorDocumentsComponent: AdjudicatorDocumentsComponent;

  constructor(
    private siteResource: SiteResource,
    private route: ActivatedRoute,
    private utilsService: UtilsService
  ) { }

  public onSaveDocuments(documentGuids: string[]) {
    const documentGuids$ = documentGuids.map(guid =>
      this.siteResource.createSiteAdjudicationDocument(this.route.snapshot.params.sid, guid));

    forkJoin(documentGuids$)
      .subscribe(val => {
        this.getDocuments();
        this.adjudicatorDocumentsComponent.removeFiles();
      });
  }

  public onGetDocumentByGuid(documentId: number) {
    this.siteResource.getSiteAdjudicationDocumentDownloadToken(this.route.snapshot.params.sid, documentId)
      .subscribe((token: string) =>
        this.utilsService.downloadToken(token)
      );
  }

  public onDeleteDocumentById(documentId: number) {
    this.busy = this.siteResource.deleteSiteAdjudicationDocument(this.route.snapshot.params.sid, documentId)
      .subscribe((document: SiteAdjudicationDocument) => this.getDocuments());
  }

  public ngOnInit(): void {
    this.getDocuments();
    this.route.params.subscribe(() => this.getDocuments());
  }

  private getDocuments() {
    this.documents$ = this.siteResource.getSiteAdjudicationDocuments(this.route.snapshot.params.sid);
  }
}
