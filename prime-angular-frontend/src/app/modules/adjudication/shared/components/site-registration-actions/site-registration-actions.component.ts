import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

import { map } from 'rxjs/operators';

import { SiteResource } from '@core/resources/site-resource.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { UtilsService } from '@core/services/utils.service';

import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';

@Component({
  selector: 'app-site-registration-actions',
  templateUrl: './site-registration-actions.component.html',
  styleUrls: ['./site-registration-actions.component.scss']
})
export class SiteRegistrationActionsComponent implements OnInit {
  @Input() siteRegistration: SiteRegistrationListViewModel;
  @Output() public approve: EventEmitter<number>;
  @Output() public decline: EventEmitter<number>;

  constructor(
    private organizationResource: OrganizationResource,
    private siteResource: SiteResource,
    private utilsService: UtilsService
  ) { }

  public getOrganizationAgreement() {
    const request$ = (this.siteRegistration.signedAgreementDocumentCount)
      ? this.organizationResource.getDownloadTokenForLatestSignedAgreement(this.siteRegistration.organizationId)
        .pipe(
          map((token: string) => this.utilsService.downloadToken(token))
        )
      : this.organizationResource.getUnsignedOrganizationAgreement()
        .pipe(
          map((base64: string) => this.utilsService.base64ToBlob(base64)),
          map((blob: Blob) => this.utilsService.downloadDocument(blob, 'Organization-Agreement'))
        );

    request$.subscribe();
  }

  public getBusinessLicence() {
    this.siteResource.getBusinessLicenceDownloadToken(this.siteRegistration.siteId)
      .pipe(
        map((token: string) => this.utilsService.downloadToken(token))
      )
      .subscribe();
  }

  public ngOnInit(): void { }
}
