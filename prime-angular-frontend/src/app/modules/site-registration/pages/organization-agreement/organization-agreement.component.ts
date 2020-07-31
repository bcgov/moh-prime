import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, EMPTY } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrganizationService } from '@registration/shared/services/organization.service';

@Component({
  selector: 'app-organization-agreement',
  templateUrl: './organization-agreement.component.html',
  styleUrls: ['./organization-agreement.component.scss']
})
export class OrganizationAgreementComponent implements OnInit, IPage {
  public busy: Subscription;
  public routeUtils: RouteUtils;
  public organizationAgreement: string;
  public hasAcceptedAgreement: boolean;
  public hasDownloadedFile: boolean;
  public hasUploadedFile: boolean;
  public hasNoUploadError: boolean;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  @ViewChild('accept') public accepted: MatCheckbox;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private organizationService: OrganizationService,
    private organizationResource: OrganizationResource,
    private organizationFormStateService: OrganizationFormStateService,
    private siteResource: SiteResource,
    private dialog: MatDialog,
    private utilsService: UtilsService
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public onSubmit() {
    if (this.accepted?.checked || this.hasUploadedFile) {
      const organizationid = this.route.snapshot.params.oid;
      const data: DialogOptions = {
        title: 'Organization Agreement',
        message: 'Are you sure you want to accept the Organization Agreement?',
        actionText: 'Accept Organization Agreement'
      };
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? this.organizationResource.acceptCurrentOrganizationAgreement(organizationid)
              : EMPTY
          ),
          exhaustMap(() => this.siteResource.updateSiteCompleted((this.route.snapshot.queryParams.siteId)))
        )
        .subscribe(() => this.nextRoute());
    }
  }

  public onUpload(event: BaseDocument) {
    const organizationId = this.organizationService.organization.id;
    this.organizationResource
      .addSignedAgreement(organizationId, event.documentGuid, event.filename)
      .subscribe();

    this.hasUploadedFile = true;
    this.hasNoUploadError = false;
  }

  public onDownload() {
    this.organizationResource
      .getUnsignedOrganizationAgreement()
      .subscribe((base64: string) => {
        const blob = this.utilsService.base64ToBlob(base64);
        this.utilsService.downloadDocument(blob, 'Organization-Agreement');
        this.hasDownloadedFile = true;
      });
  }

  public onBack() {
    const siteId = this.route.snapshot.queryParams.siteId;
    if (siteId) {
      this.routeUtils.routeRelativeTo(SiteRoutes.REMOTE_USERS);
    } else {
      this.routeUtils.routeWithin(SiteRoutes.SITE_MANAGEMENT);
    }
  }

  public nextRoute() {
    const redirectPath = this.route.snapshot.queryParams.redirect;
    if (redirectPath) {
      this.routeUtils.routeRelativeTo([redirectPath, SiteRoutes.SITE_REVIEW]);
    } else {
      this.routeUtils.routeWithin(SiteRoutes.SITE_MANAGEMENT);
    }
  }

  public showDefaultAgreement() {
    return this.organizationService.organization.signedAgreementDocuments?.length < 1 ?? true;
  }

  public downloadSignedAgreement() {
    this.organizationResource
      .getDownloadTokenForLatestSignedAgreement(this.organizationService.organization.id)
      .subscribe((token: string) => {
        this.utilsService.downloadToken(token);
      });
  }

  public ngOnInit(): void {
    const organization = this.organizationService.organization;
    this.isCompleted = organization?.completed;
    this.organizationFormStateService.setForm(organization);

    this.hasAcceptedAgreement = !!organization.acceptedAgreementDate;

    this.organizationResource
      .getOrganizationAgreement(organization.id)
      .subscribe((organizationAgreement: string) =>
        this.organizationAgreement = organizationAgreement
      );
  }
}
