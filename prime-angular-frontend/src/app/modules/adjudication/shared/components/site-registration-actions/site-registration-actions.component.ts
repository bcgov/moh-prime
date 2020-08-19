import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

import { map, exhaustMap } from 'rxjs/operators';

import { SiteResource } from '@core/resources/site-resource.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { UtilsService } from '@core/services/utils.service';

import { Site } from '@registration/shared/models/site.model';
import { Organization } from '@registration/shared/models/organization.model';

@Component({
  selector: 'app-site-registration-actions',
  templateUrl: './site-registration-actions.component.html',
  styleUrls: ['./site-registration-actions.component.scss']
})
export class SiteRegistrationActionsComponent implements OnInit {
  @Input() site: Site;
  @Output() public approve: EventEmitter<number>;
  @Output() public decline: EventEmitter<number>;

  constructor(
    private siteResource: SiteResource,
    private utilsService: UtilsService,
    private organizationResource: OrganizationResource,
  ) { }

  public getOrganizationAgreement() {
    this.organizationResource.getOrganizationById(this.site.organizationId)
      .pipe(
        exhaustMap((organization: Organization) =>
          (organization.signedAgreementDocuments.length > 0)
            ? this.organizationResource.getDownloadTokenForLatestSignedAgreement(this.site.organizationId)
              .pipe(
                map((token: string) => this.utilsService.downloadToken(token))
              )
            : this.organizationResource.getUnsignedOrganizationAgreement()
              .pipe(
                map((base64: string) => this.utilsService.base64ToBlob(base64)),
                map((blob: Blob) => this.utilsService.downloadDocument(blob, 'Organization-Agreement'))
              )
        )
      ).subscribe();
  }

  public getBusinessLicence() {
    this.siteResource.getBusinessLicenceDownloadToken(this.site.id)
      .subscribe((token: string) =>
        this.utilsService.downloadToken(token)
      );
  }

  public ngOnInit(): void { }
}
