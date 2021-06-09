import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';

import { Subject, Subscription } from 'rxjs';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { ContactFormState } from '@lib/classes/contact-form-state.class';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-technical-supports-page',
  templateUrl: './technical-supports-page.component.html',
  styleUrls: ['./technical-supports-page.component.scss']
})
export class TechnicalSupportsPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public form: FormGroup;
  public isInitialEntry: boolean;
  public contacts: Contact[];
  public formSubmittingEvent: Subject<void>;

  private routeUtils: RouteUtils;

  constructor(
    private fb: FormBuilder,
    private haResource: HealthAuthorityResource,
    private formUtilsService: FormUtilsService,
    private route: ActivatedRoute,
    router: Router
  ) {
    this.title = route.snapshot.data.title;
    this.isInitialEntry = !!route.snapshot.queryParams.initial;
    this.routeUtils = new RouteUtils(route, router, [
      AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS),
      AdjudicationRoutes.SITE_REGISTRATIONS,
      AdjudicationRoutes.HEALTH_AUTHORITIES,
      this.route.snapshot.params.haid
    ]);
    this.formSubmittingEvent = new Subject<void>();
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      // TODO perform update and route to next page
      this.nextRouteAfterSubmit();
    }
  }

  public onBack(): void {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_PRIVACY_OFFICER);
  }

  public ngOnInit(): void {
    this.createFormInstance();
  }

  private createFormInstance() {
    this.form = new ContactFormState(this.fb, this.formUtilsService).form;
  }

  private initForm() {

  }

  private nextRouteAfterSubmit() {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_ADMINISTRATORS);
  }

  private routeTo(routeSegment?: string) {
    const routePath = (this.isInitialEntry && routeSegment)
      ? routeSegment
      : AdjudicationRoutes.ORGANIZATION_INFORMATION;
    this.routeUtils.routeRelativeTo(routePath, { queryParamsHandling: 'preserve' });
  }
}
