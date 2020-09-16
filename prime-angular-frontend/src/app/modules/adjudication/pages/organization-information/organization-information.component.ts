import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Subscription, BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { AuthService } from '@auth/shared/services/auth.service';

import { Organization } from '@registration/shared/models/organization.model';
import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';

@Component({
  selector: 'app-organization-information',
  templateUrl: './organization-information.component.html',
  styleUrls: ['./organization-information.component.scss']
})
export class OrganizationInformationComponent implements OnInit {
  public busy: Subscription;
  public hasActions: boolean;
  public refresh: BehaviorSubject<boolean>;

  public organization: Organization;

  constructor(
    private route: ActivatedRoute,
    private organizationResource: OrganizationResource,
    private authService: AuthService,
    private utilsService: UtilsService
  ) {
    this.hasActions = true;
  }

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public getOrganizationAgreement(siteRegistration: SiteRegistrationListViewModel) {
    const request$ = (siteRegistration.signedAgreementDocumentCount)
      ? this.organizationResource.getDownloadTokenForLatestSignedAgreement(siteRegistration.organizationId)
        .pipe(
          map((token: string) => this.utilsService.downloadToken(token))
        )
      : this.organizationResource.getSignedOrganizationAgreement(siteRegistration.organizationId)
        .pipe(
          map((base64: string) => this.utilsService.base64ToBlob(base64)),
          map((blob: Blob) => this.utilsService.downloadDocument(blob, 'Organization-Agreement'))
        );
    request$.subscribe();
  }

  public ngOnInit(): void {
    this.busy = this.getOrganization()
      .subscribe((organization: Organization) => this.organization = organization);
  }

  private getOrganization(): Observable<Organization> {
    const organizationId = this.route.snapshot.params.oid;
    return this.organizationResource.getOrganizationById(organizationId);
  }
}
