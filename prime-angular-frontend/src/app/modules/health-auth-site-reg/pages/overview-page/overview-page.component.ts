import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup } from '@angular/forms';

import { Subscription, EMPTY, of } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { UntilDestroy } from '@ngneat/until-destroy';

import { Moment } from 'moment';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { ToastService } from '@core/services/toast.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { HealthAuthorityService } from '@health-auth/shared/services/health-authority.service';
import { HealthAuthoritySiteService } from '@health-auth/shared/services/health-authority-site.service';
import { HealthAuthoritySiteFormStateService } from '@health-auth/shared/services/health-authority-site-form-state.service';

@UntilDestroy()
@Component({
  selector: 'app-overview-page',
  templateUrl: './overview-page.component.html',
  styleUrls: ['./overview-page.component.scss']
})
export class OverviewPageComponent implements OnInit {
  public busy: Subscription;
  public pharmanetAdministrators: Contact[];
  public technicalSupports: Contact[];
  public healthAuthoritySite: HealthAuthoritySite;
  public siteIsIncomplete: boolean;
  public siteIsInReview: boolean;
  public siteIsLocked: boolean;
  public siteIsApproved: boolean;
  public siteExpiryDate: string | Moment | null;
  public showEditRedirect: boolean;
  public showSubmissionAction: boolean;
  public routeUtils: RouteUtils;
  public HealthAuthSiteRegRoutes = HealthAuthSiteRegRoutes;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private dialog: MatDialog,
    private healthAuthorityService: HealthAuthorityService,
    private healthAuthoritySiteService: HealthAuthoritySiteService,
    private healthAuthoritySiteFormStateService: HealthAuthoritySiteFormStateService,
    private healthAuthoritySiteResource: HealthAuthoritySiteResource,
    private formUtilsService: FormUtilsService,
    private toastService: ToastService
  ) {
    this.showEditRedirect = true;
    this.routeUtils = new RouteUtils(route, router, HealthAuthSiteRegRoutes.routePath(HealthAuthSiteRegRoutes.MODULE_PATH));
  }

  public onSubmit(): void {
    if (this.siteIsInReview) {
      return;
    }

    if (!this.healthAuthoritySiteFormStateService.isValidSubmission) {
      this.healthAuthoritySiteFormStateService.forms.forEach((form: FormGroup) => this.formUtilsService.logFormErrors(form));
      this.toastService.openErrorToast('Your site has an error that needs to be corrected before you will be able to submit');
      return;
    }

    const { haid, sid } = this.route.snapshot.params;
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
            ? of(this.healthAuthoritySiteFormStateService.json)
            : EMPTY
        ),
        exhaustMap((healthAuthoritySite: HealthAuthoritySite) =>
          this.healthAuthoritySiteResource.healthAuthoritySiteSubmit(haid, sid, healthAuthoritySite.forUpdate())
        )
      )
      .subscribe(() => this.nextRoute());
  }

  public onBack(): void {
    this.nextRoute();
  }

  public ngOnInit(): void {
    const healthAuthId = +this.route.snapshot.params.haid;
    const healthAuthSiteId = +this.route.snapshot.params.sid;
    if (!healthAuthId || !healthAuthSiteId) {
      return;
    }

    const { pharmanetAdministrators, technicalSupports } = this.healthAuthorityService.healthAuthority;
    this.pharmanetAdministrators = pharmanetAdministrators;
    this.technicalSupports = technicalSupports;

    // Initial assumption is the site has no pending updates, and
    // the source of truth should be used
    let healthAuthoritySite = this.healthAuthoritySiteService.site;

    this.siteIsIncomplete = healthAuthoritySite.isIncomplete();
    this.siteIsInReview = healthAuthoritySite.isInReview();
    this.siteIsLocked = healthAuthoritySite.isLocked();
    this.siteIsApproved = healthAuthoritySite.isApproved();

    this.siteExpiryDate = (healthAuthoritySite.approvedDate)
      ? healthAuthoritySite.getExpiryDate()
      : null;

    // Once submitted and under review no submissions are allowed
    this.showSubmissionAction = !this.siteIsInReview;

    if (this.healthAuthoritySiteFormStateService.isPatched) {
      // Replace site with the version from the form for the user
      // to review which maintains a subset of immutable properties
      healthAuthoritySite = this.healthAuthoritySiteFormStateService.json;
    }

    // Store a local copy of the site for overview
    this.healthAuthoritySite = healthAuthoritySite;

    // Attempt to patch the form if not already patched so
    // a validation check is against actual data, which will
    // not patch if already patched during a user update
    //
    // NOTE: Initializes the form state service for workflow
    // updates when not already patched and contains changes
    this.healthAuthoritySiteFormStateService.setForm(healthAuthoritySite);
  }

  private nextRoute(): void {
    this.routeUtils.routeTo([
      HealthAuthSiteRegRoutes.MODULE_PATH,
      HealthAuthSiteRegRoutes.SITE_MANAGEMENT
    ]);
  }
}
