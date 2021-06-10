import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';

import { BehaviorSubject, Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { Config, VendorConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { FormArrayValidators } from '@lib/validators/form-array.validators';

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
  public filteredOptions: BehaviorSubject<Config<number>[]>;
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
      console.log(this.form.value);
      const vendorCodes: number[] = this.vendors.value.map(({ code }) => code);
      this.healthAuthResource.updateVendors(this.route.snapshot.params.haid, vendorCodes)
        .subscribe(() => this.nextRouteAfterSubmit());
    }
  }

  public addVendor(vendor: string = null) {
    this.vendors.push(this.fb.group({
      vendor: [vendor ?? '', Validators.required]
    }));
  }

  public removeVendor(index: number) {
    this.vendors.removeAt(index);
  }

  public removeNone(input: HTMLInputElement) {
    // TODO likely not needed
  }

  public onBack() {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_CARE_TYPES);
  }

  public displayWith(vendor: VendorConfig): string {
    return (vendor) ? vendor.name : '';
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
    this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .subscribe(({ vendorCodes }: HealthAuthority) =>
        (vendorCodes?.length)
          ? vendorCodes.map(v => this.addVendor(v))
          : this.addVendor()
      );
  }

  private nextRouteAfterSubmit() {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_PRIVACY_OFFICER);
  }

  private routeTo(routeSegment?: string) {
    const routePath = (this.isInitialEntry && routeSegment)
      ? routeSegment
      : AdjudicationRoutes.ORGANIZATION_INFORMATION;
    this.routeUtils.routeRelativeTo(routePath, { queryParamsHandling: 'preserve' });
  }
}
