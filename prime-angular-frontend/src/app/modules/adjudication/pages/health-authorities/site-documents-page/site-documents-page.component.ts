import { ActivatedRoute } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { forkJoin, Observable, Subscription } from 'rxjs';

import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService } from '@core/services/utils.service';

import { SiteAdjudicationDocument } from '@registration/shared/models/adjudication-document.model';
import { AdjudicatorDocumentsComponent } from '@adjudication/shared/components/adjudicator-documents/adjudicator-documents.component';

@Component({
  selector: 'app-site-documents-page',
  templateUrl: './site-documents-page.component.html',
  styleUrls: ['./site-documents-page.component.scss']
})
export class SiteDocumentsPageComponent implements OnInit {
  public documents$: Observable<SiteAdjudicationDocument[]>;
  public busy: Subscription;
  @ViewChild('adjudicationDocuments') public adjudicatorDocumentsComponent: AdjudicatorDocumentsComponent;

  constructor(
    private siteResource: SiteResource,
    private route: ActivatedRoute,
    private utilsService: UtilsService
  ) { }

  public onSaveDocuments(documentGuids: string[]): void {
    const documentGuids$ = documentGuids.map(guid =>
      this.siteResource.createSiteAdjudicationDocument(+this.route.snapshot.params.sid, guid));

    forkJoin(documentGuids$)
      .subscribe(val => {
        this.getDocuments();
        this.adjudicatorDocumentsComponent.removeFiles();
      });
  }

  public onGetDocumentByGuid(documentId: number): void {
    this.siteResource.getSiteAdjudicationDocumentDownloadToken(+this.route.snapshot.params.sid, documentId)
      .subscribe((token: string) =>
        this.utilsService.downloadToken(token)
      );
  }

  public onDeleteDocumentById(documentId: number): void {
    this.busy = this.siteResource.deleteSiteAdjudicationDocument(+this.route.snapshot.params.sid, documentId)
      .subscribe((document: SiteAdjudicationDocument) => this.getDocuments());
  }

  public ngOnInit(): void {
    this.getDocuments();
    this.route.params.subscribe(() => this.getDocuments());
  }

  private getDocuments(): void {
    this.documents$ = this.siteResource.getSiteAdjudicationDocuments(+this.route.snapshot.params.sid);
  }
}
