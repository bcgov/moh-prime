import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, EMPTY } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';
import { registerPlugin } from 'ngx-filepond';
import { FilePondComponent } from 'ngx-filepond/filepond.component';
import FilePondPluginFileValidateType from 'filepond-plugin-file-validate-type';

registerPlugin(FilePondPluginFileValidateType);

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { LoggerService } from '@core/services/logger.service';
import { KeycloakTokenService } from '@auth/shared/services/keycloak-token.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';

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
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;
  public hasDownloadedFile: boolean;
  public hasUploadedFile: boolean;
  public hasNoUploadError: boolean;

  @ViewChild('accept') accepted: MatCheckbox;

  @ViewChild('filePond') public filePondComponent: FilePondComponent;
  public filePondOptions: { [key: string]: any };
  public filePondUploadProgress = 0;
  public filePondFiles = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private organizationService: OrganizationService,
    private organizationResource: OrganizationResource,
    private organizationFormStateService: OrganizationFormStateService,
    private dialog: MatDialog,
    private logger: LoggerService,
    private keycloakTokenService: KeycloakTokenService,
    private toastService: ToastService,
    private utilsService: UtilsService
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.filePondOptions = {
      class: 'prime-filepond',
      multiple: false,
      labelIdle: 'Click to Browse or Drop files here',
      acceptedFileTypes: [
        'image/jpeg',
        'image/png',
      ],
      fileValidateTypeDetectType: (source: any, type: string) => new Promise((resolve, reject) => {
        if (!type.includes('image')) {
          this.toastService.openSuccessToast('File must be image');
          reject(type);
        }
        resolve(type);
      }),
      allowFileTypeValidation: true
    };
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
          )
        )
        .subscribe(() => this.nextRoute());
    }
  }

  public onUpload(event: BaseDocument) {
    const organizationId = this.organizationService.organization.id;
    this.organizationResource
      .addSignedAgreement(organizationId, event.documentGuid, event.fileName)
      .subscribe();

    this.hasUploadedFile = true;
    this.hasNoUploadError = false;
  }

  public onDownload() {
    this.organizationResource
      .downloadOrganizationAgreement()
      .subscribe((base64: string) => {
        const blob = this.utilsService.base64ToBlob(base64);
        this.utilsService.downloadDocument(blob, 'Organization-Agreement');
        this.hasDownloadedFile = true;
      });

  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_REVIEW);
  }

  public nextRoute() {
    this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.ORGANIZATIONS], {
      queryParams: { submitted: true, signed: true }
    });
  }

  public showDefaultAgreement() {
    return this.organizationService.organization.signedAgreements?.length < 1 ?? true;
  }

  public downloadSignedAgreement() {
    this.organizationResource
      .downloadLatestSignedAgreement(this.organizationService.organization.id)
      .subscribe((base64: string) => {
        const blob = this.utilsService.base64ToBlob(base64);
        this.utilsService.downloadDocument(blob, 'Signed-organization-agreement');
      });
  }

  public ngOnInit(): void {
    // TODO structured to match in all site views
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
