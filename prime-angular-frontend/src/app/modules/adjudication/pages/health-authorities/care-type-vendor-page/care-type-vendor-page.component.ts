
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { VendorConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { ToastService } from '@core/services/toast.service';
import { HaCareTypeVendor } from '@lib/models/ha-care-type-vendor.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { HealthAuthority } from '@shared/models/health-authority.model';
import { Subscription } from 'rxjs/internal/Subscription';

@Component({
  selector: 'app-care-type-vendor-page',
  templateUrl: './care-type-vendor-page.component.html',
  styleUrls: ['./care-type-vendor-page.component.scss']
})
export class CareTypeVendorPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public form: FormGroup;
  public isInitialEntry: boolean;
  public availableVendors: VendorConfig[];
  public healthAuthority: HealthAuthority;

  private routeUtils: RouteUtils;

  constructor(
    private fb: FormBuilder,
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
  }

  public get careTypeVendors(): FormArray {
    return this.form.get('careTypeVendors') as FormArray;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form) || this.form.disabled) {

      let haCareTypeVendors: HaCareTypeVendor[];

      this.careTypeVendors.controls.forEach((c: FormControl) => {
        let vendorCodeArray = c.get("vendorCodes") as FormArray;
        let vendorCodes = vendorCodeArray.controls.map((v) => {
          let haCareTypeVendor: HaCareTypeVendor = {
            careType: c.get("careType").value,
            vendorCode: v.get("vendorCode").value
          }
          return haCareTypeVendor;
        });
        if (!haCareTypeVendors) {
          haCareTypeVendors = vendorCodes;
        } else {
          haCareTypeVendors = haCareTypeVendors.concat(vendorCodes);
        }
      });
      this.busy = this.healthAuthResource.updateHealthAuthorityCareTypeVendor(this.route.snapshot.params.haid, haCareTypeVendors)
        .subscribe(() => this.nextRouteAfterSubmit());
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      careTypeVendors: this.fb.array([])
    });
  }

  private createCareTypeFormControl() {
    return this.fb.group({
      careType: [],
      vendorCodes: this.fb.array([]),
    });
  }

  private initForm() {
    this.busy = this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .subscribe((healthAuthority) => {
        this.healthAuthority = healthAuthority;
        this.availableVendors = this.configService.vendors.filter(v => healthAuthority.vendors.some(hav => hav.vendorCode === v.code));

        if (healthAuthority.careTypes?.length) {
          healthAuthority.careTypes.forEach((careType) => {
            let careTypeVendor = this.createCareTypeFormControl();
            careTypeVendor.get('careType').patchValue(careType.careType);
            careType.vendors.forEach((vendor) => {
              let vendorArray = careTypeVendor.get('vendorCodes') as FormArray;
              vendorArray.addValidators(FormArrayValidators.noDuplicateValue('vendorCode'));
              vendorArray.push(this.fb.group({ vendorCode: [vendor.vendorCode, [Validators.required]] }));
            });
            this.careTypeVendors.push(careTypeVendor);
          })
        }
      }
      );
  }

  public addVendor(caretypeIndex: number) {
    var vendorArray = this.careTypeVendors.controls[caretypeIndex].get('vendorCodes') as FormArray;
    vendorArray.push(this.fb.group({ vendorCode: [null, [Validators.required]] }));
  }

  public removeVendor(caretypeIndex: number, vendorIndex: number) {
    var vendorArray = this.careTypeVendors.controls[caretypeIndex].get('vendorCodes') as FormArray;
    vendorArray.removeAt(vendorIndex);
  }

  public onBack() {
    this.routeTo();
  }

  private nextRouteAfterSubmit() {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_CARE_TYPE_VENDOR);
  }

  private routeTo(routeSegment?: string) {
    const routePath = (this.isInitialEntry && routeSegment)
      ? routeSegment
      : AdjudicationRoutes.ORGANIZATION_INFORMATION;
    this.routeUtils.routeRelativeTo(routePath, { queryParamsHandling: 'preserve' });
  }

  public isRequired(careTypeIndex: number, vendorIndex: number) {
    var careTypeControl = this.careTypeVendors.controls[careTypeIndex];
    var vendorCodes = careTypeControl.get("vendorCodes") as FormArray;
    return vendorCodes.controls[vendorIndex].get("vendorCode").hasError('required');
  }

  public hasDuplicate(careTypeIndex: number) {
    var careTypeControl = this.careTypeVendors.controls[careTypeIndex];
    var vendorCodes = careTypeControl.get("vendorCodes") as FormArray;
    return vendorCodes.hasError('duplicate');
  }
}
