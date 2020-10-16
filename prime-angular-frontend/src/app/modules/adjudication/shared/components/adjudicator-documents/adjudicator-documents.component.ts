import { Component, OnInit } from '@angular/core';
import { AdjudicationDocument } from '@registration/shared/models/adjudication-document.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-adjudicator-documents',
  templateUrl: './adjudicator-documents.component.html',
  styleUrls: ['./adjudicator-documents.component.scss']
})
export class AdjudicatorDocumentsComponent implements OnInit {
  public documents$: Observable<AdjudicationDocument[]>;

  constructor() {

  }

  public onSubmit() {
    // const siteId = this.route.snapshot.params.sid;
    // const hasBusinessLicence = this.businessLicenceDocuments.length || this.uploadedFile;
    // if (this.formUtilsService.checkValidity(this.form) && hasBusinessLicence) {
    //   const payload = this.siteFormStateService.json;
    //   this.siteResource
    //     .updateSite(payload)
    //     .pipe(
    //       exhaustMap(() =>
    //         (payload.businessLicenceGuid)
    //           ? this.siteResource.createBusinessLicence(siteId, payload.businessLicenceGuid)
    //           : of(noop)
    //       )
    //     )
    //     .subscribe(() => {
    //       // TODO should make this cleaner, but for now good enough
    //       // Remove the business licence GUID to prevent 404 already
    //       // submitted if resubmited in same session
    //       this.businessLicenceGuid.patchValue(null);
    //       this.form.markAsPristine();
    //       this.nextRoute();
    //     });
    // } else {
    //   if (!hasBusinessLicence) {
    //     this.hasNoBusinessLicenceError = true;
    //   }
    // }
  }

  public onUpload(document: AdjudicationDocument) {
    // this.businessLicenceGuid.patchValue(document.documentGuid);
    // this.uploadedFile = true;
    // this.hasNoBusinessLicenceError = false;
  }

  public onRemoveDocument(documentGuid: string) {
    // this.businessLicenceGuid.patchValue(null);
  }

  public getDocument(event: Event) {
    // event.preventDefault();
    // this.siteResource.getBusinessLicenceDownloadToken(this.siteService.site.id)
    //   .subscribe((token: string) =>
    //     this.utilsService.downloadToken(token)
    //   );
  }

  ngOnInit(): void {
  }

}
