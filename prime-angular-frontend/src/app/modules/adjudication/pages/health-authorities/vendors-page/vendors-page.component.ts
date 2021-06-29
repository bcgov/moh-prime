import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { BehaviorSubject, Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { VendorConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-vendors-page',
  templateUrl: './vendors-page.component.html',
  styleUrls: ['./vendors-page.component.scss']
})
export class VendorsPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public form: FormGroup;
  public isInitialEntry: boolean;
  public filteredVendors: BehaviorSubject<VendorConfig[]>;

  private routeUtils: RouteUtils;

  constructor(
    private fb: FormBuilder,
    private healthAuthResource: HealthAuthorityResource,
    private formUtilsService: FormUtilsService,
    private configService: ConfigService,
    private route: ActivatedRoute,
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
    this.filteredVendors = new BehaviorSubject<VendorConfig[]>(this.configService.vendors);
  }

  public get vendors(): FormArray {
    return this.form.get('vendors') as FormArray;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const vendorCodes = [...new Set(this.vendors.value.map(({ vendor }) => vendor.code) as number[])];
      this.healthAuthResource.updateVendors(this.route.snapshot.params.haid, vendorCodes)
        .subscribe(() => this.nextRouteAfterSubmit());
    }
  }

  public addVendor(vendor: VendorConfig = null) {
    this.vendors.push(this.fb.group({
      vendor: [vendor ?? null, Validators.required]
    }));
  }

  public removeVendor(index: number) {
    this.vendors.removeAt(index);
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
      vendors: this.fb.array([], FormArrayValidators.atLeast(1))
    });
  }

  private initForm() {
    this.form.valueChanges
      .subscribe(({ vendors }: { vendors: { vendor: VendorConfig }[] }) => {
        const selectedVendorCodes = vendors.map(ct => ct.vendor?.code);
        // Filter out the selected vendors to avoid visual duplicates
        const filteredVendors = this.configService.vendors.filter(v => !selectedVendorCodes.includes(v.code));
        this.filteredVendors.next(filteredVendors);
      });

    this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .subscribe(({ vendorCodes }: HealthAuthority) =>
        (vendorCodes?.length)
          ? this.configService.vendors
            .filter(v => vendorCodes.includes(v.code))
            .map(v => this.addVendor(v))
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
