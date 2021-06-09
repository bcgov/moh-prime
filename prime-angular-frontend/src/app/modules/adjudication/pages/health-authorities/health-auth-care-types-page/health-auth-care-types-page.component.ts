import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { BehaviorSubject, Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { Config } from '@config/config.model';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

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
  // TODO don't add these if not required for this component
  // public allowDefaultOption: boolean;
  // public defaultOptionLabel: string;

  private routeUtils: RouteUtils;

  constructor(
    private fb: FormBuilder,
    private healthAuthResource: HealthAuthorityResource,
    private formUtilsService: FormUtilsService,
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
  }

  public get careTypes(): FormArray {
    return this.form.get('careTypes') as FormArray;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      // TODO perform update and route to next page
      this.nextRouteAfterSubmit();
    }
  }

  public addCareType() {

  }

  public removeCareType(input: HTMLFormElement) {

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
      careTypes: this.fb.array([])
    });
  }

  private initForm() {

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
