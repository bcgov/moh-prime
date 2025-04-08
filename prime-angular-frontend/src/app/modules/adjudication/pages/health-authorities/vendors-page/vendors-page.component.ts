import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormArray, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';

import { BehaviorSubject, Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { VendorConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthority } from '@shared/models/health-authority.model';
import { HealthAuthorityVendor } from '@health-auth/shared/models/health-authority-vendor.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

interface HealthAuthorityVendorMap extends VendorConfig {
  id?: number;
}
@Component({
  selector: 'app-vendors-page',
  templateUrl: './vendors-page.component.html',
  styleUrls: ['./vendors-page.component.scss']
})
export class VendorsPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public form: UntypedFormGroup;
  public isInitialEntry: boolean;
  public filteredVendors: BehaviorSubject<HealthAuthorityVendorMap[]>;
  public healthAuthorityVendors: HealthAuthorityVendorMap[];

  private routeUtils: RouteUtils;

  constructor(
    private fb: UntypedFormBuilder,
    private healthAuthResource: HealthAuthorityResource,
    private formUtilsService: FormUtilsService,
    private configService: ConfigService,
    private route: ActivatedRoute,
    private toastService: ToastService,
    router: Router
  ) {
    this.title = route.snapshot.data.title;
    this.isInitialEntry = !!this.route.snapshot.queryParams.initial;
    this.routeUtils = new RouteUtils(route, router, [
      AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS),
      AdjudicationRoutes.SITE_REGISTRATIONS,
      AdjudicationRoutes.HEALTH_AUTHORITIES,
      this.route.snapshot.params.haid
    ]);

    this.healthAuthorityVendors = this.configService.vendors
      .filter((vendorConfig: VendorConfig) => vendorConfig.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY) as HealthAuthorityVendorMap[];

    this.filteredVendors = new BehaviorSubject<HealthAuthorityVendorMap[]>(this.healthAuthorityVendors);
  }

  public get vendors(): UntypedFormArray {
    return this.form.get('vendors') as UntypedFormArray;
  }

  public onSubmit() {
    // A disabled form signifies no changes
    if (this.formUtilsService.checkValidity(this.form) || this.form.disabled) {
      const vendorCodes = [...new Set(this.vendors.getRawValue().map(({ vendor }) => vendor.code) as number[])];
      this.busy = this.healthAuthResource.updateHealthAuthorityVendors(this.route.snapshot.params.haid, vendorCodes)
        .subscribe(() => this.nextRouteAfterSubmit());
    }
  }

  public addVendor(vendor: HealthAuthorityVendorMap = null) {
    this.vendors.push(this.fb.group({
      vendor: [{ value: vendor, disabled: vendor?.id }, Validators.required]
    }));
  }

  public removeVendor(index: number) {
    const vendorId = this.vendors.getRawValue()[index]?.vendor?.id;

    if (!vendorId) {
      // when vendorId is undefined that means we're deleting a vendor after adding it and before hitting Save and Continue
      // i.e. before it was given an Id
      this.vendors.removeAt(index);
      return;
    }

    this.busy = this.healthAuthResource.getHealthAuthorityVendorSiteIds(this.route.snapshot.params.haid, vendorId)
      .subscribe((healthAuthoritySites) => {
        (!healthAuthoritySites.length)
          ? this.vendors.removeAt(index)
          : this.toastService.openErrorToast('Vendor could not be removed, one or more sites are using it');
      });
  }

  public onBack() {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_CARE_TYPES);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      vendors: this.fb.array([])
    });
  }

  private initForm() {
    this.form.valueChanges
      .subscribe(() => {
        const selectedVendorCodes = this.vendors.getRawValue().map(v => v.vendor?.code);
        // Filter out the selected vendors to avoid visual duplicates
        const filteredVendors = this.healthAuthorityVendors.filter(v => !selectedVendorCodes.includes(v.code));
        this.filteredVendors.next(filteredVendors);
      });

    this.busy = this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .subscribe(({ vendors }: HealthAuthority) =>
        (vendors?.length)
          ? vendors.map((vendor: HealthAuthorityVendor) =>
            this.addVendor({
              ...this.healthAuthorityVendors.find((v) => v.code === vendor.vendorCode),
              id: vendor.id
            })
          )
          : this.addVendor()
      );
  }

  private nextRouteAfterSubmit() {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_PRIVACY_OFFICE);
  }

  private routeTo(routeSegment?: string) {
    const routePath = (this.isInitialEntry && routeSegment)
      ? routeSegment
      : AdjudicationRoutes.ORGANIZATION_INFORMATION;
    this.routeUtils.routeRelativeTo(routePath, { queryParamsHandling: 'preserve' });
  }
}
