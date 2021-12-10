
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BehaviorSubject, Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { ConfigService } from '@config/config.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { FormArrayValidators } from '@lib/validators/form-array.validators';

interface HealthAuthorityCareTypeMap {
  id?: number;
  name: string;
  code: number;
}
@Component({
  selector: 'app-health-auth-care-types-page',
  templateUrl: './health-auth-care-types-page.component.html',
  styleUrls: ['./health-auth-care-types-page.component.scss']
})
export class HealthAuthCareTypesPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public form: FormGroup;
  public isInitialEntry: boolean;
  public filteredCareTypes: BehaviorSubject<HealthAuthorityCareTypeMap[]>;

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
    this.filteredCareTypes = new BehaviorSubject<HealthAuthorityCareTypeMap[]>(this.configService.careTypes.map(ct => { return { name: ct.name, code: ct.code } }));
  }

  public get careTypes(): FormArray {
    return this.form.get('careTypes') as FormArray;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const careTypes = [...new Set(this.careTypes.getRawValue()
        .map(({ careType }) => careType.name.trim()) as string[])];
      this.healthAuthResource.updateHealthAuthorityCareTypes(this.route.snapshot.params.haid, careTypes)
        .subscribe(() => this.nextRouteAfterSubmit());
    }
  }

  public addCareType(careType: HealthAuthorityCareTypeMap = null) {
    this.careTypes.push(this.fb.group({
      careType: [{ value: careType, disabled: careType?.id }, Validators.required]
    }));
  }

  public removeCareType(index: number) {
    const careTypeId = this.careTypes.value[index]?.careType?.id;
    if (careTypeId) {
      this.healthAuthResource.getHealthAuthorityCareTypeSiteIds(this.route.snapshot.params.haid, careTypeId)
        .subscribe((healthAuthoritySites) => {
          (!healthAuthoritySites.length)
            ? this.careTypes.removeAt(index)
            : this.toastService.openErrorToast('Care type could not be removed, one or more sites are using it');
        });
    } else {
      // when careTypeId is undefined that means we're deleting a care type after adding it and before hitting Save and Continue
      // i.e. before it was given an Id
      this.careTypes.removeAt(index);
    }
  }

  public onBack() {
    this.routeTo();
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      careTypes: this.fb.array([], FormArrayValidators.atLeast(1))
    });
  }

  private initForm() {
    this.form.valueChanges
      .subscribe(() => {
        const selectedCareTypes = this.careTypes.getRawValue().map(ct => ct.careType?.name);
        const filteredCareTypes = this.configService.careTypes
          .filter(ct => !selectedCareTypes.includes(ct.name));
        this.filteredCareTypes.next(filteredCareTypes);
      });

    this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .subscribe(({ careTypes }: HealthAuthority) => {
        if (careTypes?.length) {
          careTypes.map((careType) => this.addCareType({
            ...this.configService.careTypes.find((ct) => ct.name === careType.careType),
            id: careType.id
          }));
        } else {
          this.addCareType();
        }
      });
  }

  private nextRouteAfterSubmit() {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_VENDORS);
  }

  private routeTo(routeSegment?: string) {
    const routePath = (this.isInitialEntry && routeSegment)
      ? routeSegment
      : AdjudicationRoutes.ORGANIZATION_INFORMATION;
    this.routeUtils.routeRelativeTo(routePath, { queryParamsHandling: 'preserve' });
  }
}
