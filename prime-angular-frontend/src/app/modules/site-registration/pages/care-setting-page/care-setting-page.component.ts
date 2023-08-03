import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { noop, of } from 'rxjs';
import { exhaustMap, map, pairwise, startWith, tap } from 'rxjs/operators';

import { Config, VendorConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { Role } from '@auth/shared/enum/role.enum';
import { PermissionService } from '@auth/shared/services/permission.service';

import { AbstractCommunitySiteRegistrationPage } from '@registration/shared/classes/abstract-community-site-registration-page.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { CareSettingPageFormState } from './care-setting-page-form-state.class';
import { NoContent } from '@core/resources/abstract-resource';
import { IStep } from '@shared/components/progress-indicator/progress-indicator.component';

@UntilDestroy()
@Component({
  selector: 'app-care-setting-page',
  templateUrl: './care-setting-page.component.html',
  styleUrls: ['./care-setting-page.component.scss']
})
export class CareSettingPageComponent extends AbstractCommunitySiteRegistrationPage implements OnInit {
  public formState: CareSettingPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public isSubmitted: boolean;
  public organization: string;
  public careSettingConfig: Config<number>[];
  public vendorConfig: VendorConfig[];
  public filteredVendorConfig: VendorConfig[];
  public hasNoVendorError: boolean;
  public SiteRoutes = SiteRoutes;
  public siteSteps: IStep[];

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected siteService: SiteService,
    protected siteFormStateService: SiteFormStateService,
    protected siteResource: SiteResource,
    private configService: ConfigService,
    private permissionService: PermissionService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService, siteService, siteFormStateService, siteResource);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.careSettingConfig = this.configService.careSettings;
    this.vendorConfig = this.configService.vendors;
    this.hasNoVendorError = false;
    this.filteredVendorConfig = [];
  }

  public enableCareSetting(careSettingCode: number): boolean {
    switch (careSettingCode) {
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE:
        return true;
      case CareSettingEnum.COMMUNITY_PHARMACIST:
        return true;
      case CareSettingEnum.DEVICE_PROVIDER:
        return this.permissionService.hasRoles(Role.FEATURE_SITE_DEVICE_PROVIDER);
      default:
        return false;
    }
  }

  public onBack() {
    (!this.isCompleted)
      ? this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.ORGANIZATIONS])
      : this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  protected createFormInstance() {
    this.formState = this.siteFormStateService.careSettingPageFormState;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.isSubmitted = site?.submittedDate ? true : false;
    this.siteFormStateService.setForm(site, !this.hasBeenSubmitted);
    this.formState.form.markAsPristine();
  }

  protected initForm() {
    this.formState.careSettingCode.valueChanges
      .pipe(
        untilDestroyed(this),
        startWith([null]),
        pairwise(),
        exhaustMap(([prevCareSettingCode, nextCareSettingCode]: [number, number]) => {

          switch (nextCareSettingCode) {
            case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE:
              this.siteSteps = SiteRoutes.pchpSiteSteps();
              break;
            case CareSettingEnum.COMMUNITY_PHARMACIST:
              this.siteSteps = SiteRoutes.pharmacySiteSteps();
              break;
            case CareSettingEnum.DEVICE_PROVIDER:
              this.siteSteps = SiteRoutes.deviceProviderSiteSteps();
              break;
          }

          const deferredLicenceReason = this.siteFormStateService.businessLicenceFormState.deferredLicenceReason;

          const allowableDeferedCareSettings = [CareSettingEnum.COMMUNITY_PHARMACIST, CareSettingEnum.DEVICE_PROVIDER];
          // Reset the deferred licence reason when changing from a care setting that can defer business
          // licence upload to one that can't.
          if (
            !allowableDeferedCareSettings.includes(prevCareSettingCode)
            || allowableDeferedCareSettings.includes(nextCareSettingCode)
            || !deferredLicenceReason.value
            // When a document has been uploaded the business licence can no longer be updated
            || this.siteService.site?.businessLicence?.completed
          ) {
            return of(nextCareSettingCode); // No reset required
          }

          const { id, businessLicence, completed } = this.siteService.site;
          businessLicence.deferredLicenceReason = null;
          return this.siteResource.updateBusinessLicence(id, businessLicence)
            .pipe(
              // When not reset prevents interaction with specific controls on business licence
              tap(() => this.siteFormStateService.businessLicenceFormState.deferredLicenceReason.reset()),
              exhaustMap(() => {
                // Do nothing if not completed, but when site is marked as completed reset it
                // to force user through the wizard to ensure a business licence is uploaded
                if (!completed) {
                  return of(noop);
                }

                return this.siteResource.removeSiteCompleted(id)
                  // Will ensure that the user is routed to the next page, and not
                  // overview if previously completed
                  .pipe(tap(() => this.isCompleted = false));
              }),
              map(() => nextCareSettingCode)
            );
        }),
        map((careSettingCode: CareSettingEnum) =>
          this.vendorConfig.filter((vendorConfig: VendorConfig) => vendorConfig.careSettingCode === careSettingCode)
        )
      )
      .subscribe((vendors: VendorConfig[]) => {
        this.filteredVendorConfig = vendors;
        this.formState.vendorCode.patchValue(null);
      });

    this.patchForm();
  }

  protected submissionRequest(): NoContent {
    if (this.formState.json.careSettingCode !== CareSettingEnum.DEVICE_PROVIDER) {
      this.siteFormStateService.deviceProviderFormState.clearIndividualDeviceProviders();
    }
    if (this.formState.json.careSettingCode === CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE) {
      this.siteFormStateService.businessLicenceFormState.resetSiteId();
    }

    const payload = this.siteFormStateService.json;
    return this.siteResource.updateSite(payload);
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoVendorError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (!this.formState.vendorCode.value) {
      this.hasNoVendorError = true;
    }
  }

  protected afterSubmitIsSuccessful(): void {
    const routePath = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.BUSINESS_LICENCE;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
