import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatRadioChange } from '@angular/material/radio';

import { Observable, EMPTY } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { Config, VendorConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { VendorEnum } from '@shared/enums/vendor.enum';
import { Role } from '@auth/shared/enum/role.enum';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { PermissionService } from '@auth/shared/services/permission.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { CareSettingPageFormState } from './care-setting-page-form-state.class';

@Component({
  selector: 'app-care-setting-page',
  templateUrl: './care-setting-page.component.html',
  styleUrls: ['./care-setting-page.component.scss']
})
export class CareSettingPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: CareSettingPageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public organization: string;
  public careSettingConfig: Config<number>[];
  public vendorConfig: VendorConfig[];
  public filteredVendorConfig: VendorConfig[];
  public hasNoVendorError: boolean;
  public vendorChangeDialogOptions: DialogOptions;
  public isNewSite: FormControl;
  public SiteRoutes = SiteRoutes;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    private configService: ConfigService,
    private permissionService: PermissionService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.careSettingConfig = this.configService.careSettings;
    this.vendorConfig = this.configService.vendors;
    this.hasNoVendorError = false;
    this.vendorChangeDialogOptions = {
      title: 'Vendor Change',
      message: 'CareConnect does not support remote access to PharmaNet, all the remote practitioners you have submitted in the application will be deleted and do not have permission to access PharmaNet remotely.'
    };
    this.filteredVendorConfig = [];
    this.isNewSite = new FormControl(false);
  }

  public get careSettingCode(): FormControl {
    return this.form.get('careSettingCode') as FormControl;
  }

  public get vendorCode(): FormControl {
    return this.form.get('vendorCode') as FormControl;
  }

  public get pec(): FormControl {
    return this.form.get('pec') as FormControl;
  }

  public onVendorChange(change: MatRadioChange) {
    this.hasNoVendorError = false;

    if (change.value === VendorEnum.CARECONNECT && this.siteFormStateService.json.remoteUsers.length) {
      const data: DialogOptions = {
        icon: 'announcement',
        ...this.vendorChangeDialogOptions,
        actionText: 'Ok',
        cancelHide: true
      };
      this.dialog.open(ConfirmDialogComponent, { data });
    }
  }

  public enableCareSetting(careSettingCode: number): boolean {
    switch (careSettingCode) {
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE:
        return true;
      case CareSettingEnum.COMMUNITY_PHARMACIST:
        return this.permissionService.hasRoles(Role.FEATURE_SITE_PHARMACIST);
      default:
        return false;
    }
  }

  public onBack() {
    this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.SITE_MANAGEMENT]);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  protected createFormInstance() {
    this.formState = this.siteFormStateService.careSettingPageFormState;
    this.form = this.formState.form;
  }

  protected patchForm(): void {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.siteFormStateService.setForm(site, true);
    this.isNewSite.setValue(this.pec.disabled);
    this.form.markAsPristine();
  }

  protected initForm() {
    this.careSettingCode.valueChanges
      .pipe(
        map((careSettingCode: number) =>
          this.vendorConfig.filter(
            (vendorConfig: VendorConfig) =>
              vendorConfig.careSettingCode === careSettingCode
          )
        )
      ).subscribe((vendors: VendorConfig[]) => {
        this.filteredVendorConfig = vendors;
        this.vendorCode.patchValue(null);
      });

    this.isNewSite.valueChanges
      .subscribe(value => {
        if (value) {
          this.pec.patchValue(null);
          this.pec.disable();
        }
        else {
          this.pec.enable();
        }
      });

    this.patchForm();
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoVendorError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (!this.vendorCode.value) {
      this.hasNoVendorError = true;
    }
  }

  protected performSubmission(): Observable<unknown> {
    const payload = this.siteFormStateService.json;
    const data: DialogOptions = {
      ...this.vendorChangeDialogOptions,
      actionType: 'warn',
      actionText: 'Continue'
    };
    const update$ = this.siteResource.updateSite(payload);
    return (payload.siteVendors[0].vendorCode === VendorEnum.CARECONNECT && payload.remoteUsers.length)
      ? this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) => {
            if (!result) {
              return EMPTY;
            }

            payload.remoteUsers = [];
            return update$;
          })
        )
      : update$;
  }

  protected afterSubmitIsSuccessful(): void {
    const routePath = (this.isCompleted)
      ? SiteRoutes.SITE_REVIEW
      : SiteRoutes.BUSINESS_LICENCE;

    this.routeUtils.routeRelativeTo(routePath);
  }
}
