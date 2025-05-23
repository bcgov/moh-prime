import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormGroup, ValidationErrors } from '@angular/forms';

import { EMPTY, Observable, of, Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { Moment } from 'moment';

import { BUSY_SUBMISSION_MESSAGE } from '@lib/constants';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { SiteResource } from '@core/resources/site-resource.service';
import { BusyService } from '@lib/modules/ngx-busy/busy.service';
import { ToastService } from '@core/services/toast.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteService } from '@registration/shared/services/site.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

@Component({
  selector: 'app-overview-page',
  templateUrl: './overview-page.component.html',
  styleUrls: ['./overview-page.component.scss']
})
export class OverviewPageComponent implements OnInit {
  public busy: Subscription;
  public overviewType: 'organization' | 'site';
  public organizationId: number;
  public organization: Organization | null;
  public site: Site | null;
  public siteExpiryDate: string | Moment | null;
  public isEditable: boolean;
  public showSubmissionAction: boolean;
  public routeUtils: RouteUtils;
  public siteErrors: ValidationErrors;
  public isBusinessLicenceUpdated: boolean;
  public uploadedFilename: string;
  public isCompleted: boolean;

  public SiteRoutes = SiteRoutes;
  public SiteStatusType = SiteStatusType;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private siteResource: SiteResource,
    private dialog: MatDialog,
    private siteService: SiteService,
    private siteFormStateService: SiteFormStateService,
    private organizationService: OrganizationService,
    private toastService: ToastService,
    private formUtilsService: FormUtilsService,
    private busyService: BusyService
  ) {
    this.overviewType = (this.route.snapshot.routeConfig.path === SiteRoutes.ORGANIZATION_REVIEW)
      ? 'organization'
      : 'site';
    this.routeUtils = (this.overviewType === 'organization')
      ? new RouteUtils(route, router, SiteRoutes.routePath(SiteRoutes.ORGANIZATION_REVIEW))
      : new RouteUtils(route, router, SiteRoutes.routePath(SiteRoutes.SITE_REVIEW));
  }

  /**
   * @description
   * Highest-level check for whether the redirect actions
   * should be visible.
   *
   * NOTE: Each section within the overview applies further
   * limitations to visibility based on the site information.
   */
  public get showEditRedirect() {
    return (
      (this.overviewType === 'organization' && !this.organization.hasSubmittedSite) ||
      this.overviewType === 'site'
    );
  }

  public onSubmit(): void {
    if (!this.isEditable) {
      return;
    }

    if (!this.siteFormStateService.isValidSubmission) {
      this.siteFormStateService.forms.forEach((form: UntypedFormGroup) => this.formUtilsService.logFormErrors(form));
      this.toastService.openErrorToast('Your site has an error that needs to be corrected before you will be able to submit');
      return;
    }

    const data: DialogOptions = {
      title: 'Save Site',
      message: 'When your site is saved, it will be submitted for review.',
      actionText: 'Save Site'
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) =>
          (result)
            ? of([
              this.siteFormStateService.json,
              this.siteFormStateService.businessLicenceFormState.businessLicenceGuid.value
            ])
            : EMPTY
        ),
        this.busyService.showMessagePipe(
          BUSY_SUBMISSION_MESSAGE,
          ([updatedSite, uploadedDocumentGuid]: [Site, string]) => {
            const { businessLicence, ...remainder } = updatedSite;
            const documentGuid = (uploadedDocumentGuid) ? uploadedDocumentGuid : null;
            const payload = {
              ...remainder,
              businessLicence: {
                ...businessLicence,
                documentGuid
              }
            };
            return this.siteResource.submitSite(this.siteService.site.id, payload);
          })
      ).subscribe(() => this.nextRoute());
  }

  public onRoute(routePath: string): void {
    this.routeUtils.routeRelativeTo(routePath);
  }

  public onBack(): void {
    this.routeUtils.routeTo([
      SiteRoutes.MODULE_PATH,
      SiteRoutes.ORGANIZATIONS
    ]);
  }

  public nextRoute(): void {
    this.busy = this.isOrgAgreementRequired$()
      .subscribe((wasRequired: boolean) =>
        (wasRequired || this.site.approvedDate != null)
          ? this.routeUtils.routeRelativeTo(SiteRoutes.NEXT_STEPS)
          : this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.ORGANIZATIONS])
      );
  }

  public hasErrors(): boolean {
    return this.siteErrors ? Object.values(this.siteErrors).some(val => val) : false;
  }

  public ngOnInit(): void {
    this.organization = (
      this.overviewType === 'organization' ||
      // No sites on an organization indicates initial enrolment, which
      // should display organization and site information
      (this.overviewType === 'site' && !this.organizationService.organization.hasSubmittedSite)
    ) ? this.organizationService.organization
      : null;

    if (this.overviewType === 'organization') {
      return;
    }

    // Initial assumption is the site has no pending updates, and
    // the source of truth should be used
    let site = this.siteService.site;

    // Using the source of truth set values that the user has
    // no control over when interacting with the application
    this.isEditable = site.status === SiteStatusType.EDITABLE;
    // Submissions are allowed only if site is in editable state
    this.showSubmissionAction = this.isEditable;
    this.isCompleted = site.submittedDate ? true : false;

    this.siteExpiryDate = (site.approvedDate)
      ? Site.getExpiryDate(site)
      : null;

    if (this.siteFormStateService.isPatched) {
      // Replace site with the version from the form for the user
      // to review, but maintain a subset of immutable properties
      const { status, submittedDate, approvedDate } = site;
      site = {
        ...this.siteFormStateService.json,
        status,
        submittedDate,
        approvedDate
      };
    }

    // Store a local copy of the site for overview
    this.site = site;

    // Attempt to patch the form if not already patched so
    // a validation check is against actual data, which will
    // not patch if already patched during a user update
    //
    // NOTE: Initializes the form state service for workflow
    // updates when not already patched and contains changes
    this.siteFormStateService.setForm(site);

    this.isBusinessLicenceUpdated = this.siteFormStateService.businessLicenceFormState.isBusinessLicenceUpdated
      || (this.siteFormStateService.businessLicenceFormState.businessLicenceGuid.value && !this.site.businessLicence?.businessLicenceDocument);

    this.uploadedFilename = this.siteFormStateService.businessLicenceFormState.filename.value;

    this.siteErrors = this.getSiteErrors(site);
  }

  /**
   * @description
   * Check whether an organization agreement is required for a site based on
   * whether a care setting already exists of the same type since each
   * unique care setting type requires an organization agreement.
   */
  private isOrgAgreementRequired$(): Observable<boolean> {
    const currentCareSettingCode = this.site.careSettingCode;
    return this.siteResource.getSites(this.organizationService.organization.id)
      .pipe(
        map(sites => sites.filter(site => site.careSettingCode === currentCareSettingCode).length < 2)
      );
  }

  /**
   * @description
   * Get a set of site errors.
   *
   * NOTE: Not possible to validate some form states due to validators
   * being dynamically applied when the view is loaded. Use the passed
   * site for checking validation instead of form state.
   */
  private getSiteErrors(site: Site): ValidationErrors {
    return {
      missingBusinessLicenceOrReason: !site.businessLicence?.businessLicenceDocument
        && !site.businessLicence?.deferredLicenceReason && !this.isBusinessLicenceUpdated
    };
  }
}
