import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatRadioChange } from '@angular/material/radio';

import { Subscription, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Config, VendorConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { VendorEnum } from '@shared/enums/vendor.enum';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';

@Component({
  selector: 'app-care-setting',
  templateUrl: './care-setting.component.html',
  styleUrls: ['./care-setting.component.scss']
})
export class CareSettingComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public organization: string;
  public careSettingConfig: Config<number>[];
  public vendorConfig: VendorConfig[];
  public filteredVendorConfig: VendorConfig[];
  public hasNoVendorError: boolean;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog,
    private configService: ConfigService
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.careSettingConfig = this.configService.careSettings;
    this.vendorConfig = this.configService.vendors;
    this.hasNoVendorError = false;
  }

  public get careSettingCode(): FormControl {
    return this.form.get('careSettingCode') as FormControl;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const payload = this.siteFormStateService.json;
      this.siteResource
        .updateSite(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    } else {
      this.hasNoVendorError = true;
    }
  }

  public onVendorChange(change: MatRadioChange) {
    this.hasNoVendorError = false;

    if (change.value === VendorEnum.CARECONNECT && this.siteFormStateService.json.remoteUsers.length) {
      const data: DialogOptions = {
        icon: 'announcement',
        title: 'Vendor Change',
        message: `CareConnect does not support remote access to PharmaNet, all the remote
                  practitioners you have submitted in the application will be deleted and
                  do not have permission to access PharmaNet remotely.`,
        actionText: 'Ok',
        cancelHide: true
      };
      this.dialog.open(ConfirmDialogComponent, { data });
    }
  }

  public disableCareSetting(careSettingCode: number): boolean {
    return ![
      // Omit care setting types that are not:
      CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE,
      CareSettingEnum.COMMUNITY_PHARMACIST
    ].includes(careSettingCode);
  }

  public onBack() {
    this.routeUtils.routeTo([SiteRoutes.MODULE_PATH, SiteRoutes.SITE_MANAGEMENT]);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.BUSINESS_LICENCE);
    }
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.siteFormStateService.careSettingTypeForm;
  }

  private initForm() {
    this.careSettingCode.valueChanges
      .pipe(
        map((careSettingCode: number) =>
          this.vendorConfig.filter(
            (vendorConfig: VendorConfig) =>
              vendorConfig.careSettingCode === careSettingCode
          )
        )
      ).subscribe((vendors: VendorConfig[]) => this.filteredVendorConfig = vendors);

    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    this.siteFormStateService.setForm(site, true);
    this.form.markAsPristine();
  }
}
