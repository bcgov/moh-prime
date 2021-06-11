import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BehaviorSubject, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { FormArrayValidators } from '@lib/validators/form-array.validators';

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
  public filteredOptions: BehaviorSubject<Config<number>[]>;
  public filteredCareTypes: BehaviorSubject<Config<number>[]>;

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
    this.filteredCareTypes = new BehaviorSubject<Config<number>[]>(this.configService.careTypes);
  }

  public get careTypes(): FormArray {
    return this.form.get('careTypes') as FormArray;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const careTypes: string[] = this.careTypes.value.map(({ careType }) => careType.trim());
      this.healthAuthResource.updateCareTypes(this.route.snapshot.params.haid, careTypes)
        .subscribe(() => this.nextRouteAfterSubmit());
    }
  }

  public addCareType(careType: string = null) {
    this.careTypes.push(this.fb.group({
      careType: [careType ?? '', Validators.required]
    }));
  }

  public removeCareType(index: number) {
    this.careTypes.removeAt(index);
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
      .subscribe(({ careTypes }: { careTypes: { careType: string }[] }) =>
        this.filteredCareTypes.next(this.filterCareTypes(careTypes.map(ct => ct.careType)))
      );

    this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .subscribe(({ careTypes }: HealthAuthority) =>
        (careTypes?.length)
          ? careTypes.map(ct => this.addCareType(ct))
          : this.addCareType()
      );
  }

  private nextRouteAfterSubmit() {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_VENDORS);
  }

  private filterCareTypes(careTypes: string[]) {
    return this.configService.careTypes.filter(ct => !careTypes.includes(ct.name));
  }

  private routeTo(routeSegment?: string) {
    const routePath = (this.isInitialEntry && routeSegment)
      ? routeSegment
      : AdjudicationRoutes.ORGANIZATION_INFORMATION;
    this.routeUtils.routeRelativeTo(routePath, { queryParamsHandling: 'preserve' });
  }
}
