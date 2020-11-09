import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Subscription, BehaviorSubject, Observable } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { AgreementType } from '@shared/enums/agreement-type.enum';
import { OrganizationAgreement, OrganizationAgreementViewModel } from '@shared/models/agreement.model';

import { AuthService } from '@auth/shared/services/auth.service';

import { Organization } from '@registration/shared/models/organization.model';

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
  public organizationAgreements: OrganizationAgreementViewModel[];
  public AgreementType = AgreementType;

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

  public viewAgreement(organization: Organization, organizationAgreement: OrganizationAgreementViewModel): void {
    const request$ = (organizationAgreement?.signedAgreementDocumentGuid)
      ? this.organizationResource.getSignedOrganizationAgreementToken(organization.id, organizationAgreement.id)
        .pipe(
          map((token: string) => this.utilsService.downloadToken(token))
        )
      : this.organizationResource.getOrganizationAgreement(organization.id, organizationAgreement.id, true)
        .pipe(
          map((agreement: OrganizationAgreement) => agreement.agreementContent),
          map((base64: string) => this.utilsService.base64ToBlob(base64)),
          map((blob: Blob) => this.utilsService.downloadDocument(blob, 'Organization-Agreement'))
        );

    this.busy = request$.subscribe();
  }

  public ngOnInit(): void {
    this.busy = this.getOrganization()
      .pipe(
        map((organization: Organization) => this.organization = organization),
        exhaustMap((organization: Organization) =>
          this.organizationResource.getOrganizationAgreements(organization.id)
        )
      )
      .subscribe((agreements: OrganizationAgreementViewModel[]) =>
        this.organizationAgreements = agreements
      );
  }

  private getOrganization(): Observable<Organization> {
    const organizationId = this.route.snapshot.params.oid;
    return this.organizationResource.getOrganizationById(organizationId);
  }
}
