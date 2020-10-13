import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatCheckbox } from '@angular/material/checkbox';
import { MatDialog } from '@angular/material/dialog';
import { FormControl, FormGroup } from '@angular/forms';

import { Subscription, EMPTY, of, noop } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import * as moment from 'moment';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { OrganizationAgreement } from '@shared/models/agreement.model';

import { SiteRoutes } from '@registration/site-registration.routes';
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
  public form: FormGroup;
  public routeUtils: RouteUtils;
  public agreementId: number;
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

  public get organizationAgreementGuid(): FormControl {
    return this.form.get('organizationAgreementGuid') as FormControl;
  }

  public onSubmit() {
    if (this.accepted?.checked || this.hasUploadedFile) {
      const organizationId = this.route.snapshot.params.oid;
      const data: DialogOptions = {
        title: 'Organization Agreement',
        message: 'Are you sure you want to accept the Organization Agreement?',
        actionText: 'Accept Organization Agreement'
      };
      const payload = this.organizationFormStateService.json;
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? of(noop)
              : EMPTY
          ),
          exhaustMap(() =>
            (payload.organizationAgreementGuid)
              ? this.organizationResource.acceptOrganizationAgreement(organizationId, this.agreementId, payload.organizationAgreementGuid)
              : of(noop)
          ),
          exhaustMap(() => this.siteResource.updateCompleted((this.route.snapshot.queryParams.siteId)))
        )
        .subscribe(() => {
          // TODO should make this cleaner, but for now good enough
          // Remove the org agreement GUID to prevent 404 already
          // submitted if resubmited in same session
          this.organizationAgreementGuid.patchValue(null);
          this.nextRoute();
        });
    }
  }

  public onDownload() {
    this.organizationResource
      .getOrganizationAgreement(this.route.snapshot.params.oid, this.agreementId, true)
      .subscribe(({ agreementMarkup }: OrganizationAgreement) => {
        const blob = this.utilsService.base64ToBlob(agreementMarkup);
        this.utilsService.downloadDocument(blob, 'Organization-Agreement');
        this.hasDownloadedFile = true;
      });
  }

  public onUpload(document: BaseDocument) {
    this.organizationAgreementGuid.patchValue(document.documentGuid);
    this.hasUploadedFile = true;
    this.hasNoUploadError = false;
  }

  public onRemoveDocument(documentGuid: string) {
    this.organizationAgreementGuid.patchValue(null);
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

  public onBack() {
    const siteId = this.route.snapshot.queryParams.siteId;
    if (siteId) {
      this.routeUtils.routeRelativeTo([SiteRoutes.SITES, siteId, SiteRoutes.TECHNICAL_SUPPORT]);
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

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.organizationFormStateService.organizationAgreementForm;
  }

  private initForm() {
    const organization = this.organizationService.organization;
    const siteId = this.route.snapshot.queryParams.siteId;
    this.isCompleted = organization?.completed;
    this.organizationFormStateService.setForm(organization);

    this.hasAcceptedAgreement = !!organization.acceptedAgreementDate;

    this.busy = this.organizationResource
      .updateOrganizationAgreement<OrganizationAgreement>(organization.id, siteId)
      .pipe(
        map(({ id }: OrganizationAgreement) =>
          this.agreementId = id
        ),
        exhaustMap((agreementId: number) =>
          this.organizationResource.getOrganizationAgreement(organization.id, agreementId)
        ),
        map(({ acceptedDate, agreementMarkup }: OrganizationAgreement) => {
          const date = (acceptedDate)
            ? moment(acceptedDate).local()
            : moment();

          return [agreementMarkup, date];
        }),
        map(([organizationAgreement, date]: [string, moment.Moment]) =>
          organizationAgreement
            .replace('$day', `${date.format('dddd')}`)
            .replace('$month', date.format('MMMM'))
            .replace('$year', `${date.format('YYYY')}`)
        )
      )
      .subscribe((organizationAgreement: string) =>
        this.organizationAgreement = organizationAgreement
      );
  }
}
