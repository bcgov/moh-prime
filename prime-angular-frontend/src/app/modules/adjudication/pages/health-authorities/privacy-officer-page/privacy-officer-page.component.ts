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
import { HealthAuthority } from '@shared/models/health-authority.model';

@Component({
  selector: 'app-privacy-officer-page',
  templateUrl: './privacy-officer-page.component.html',
  styleUrls: ['./privacy-officer-page.component.scss']
})
export class PrivacyOfficerPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public form: FormGroup;
  public isInitialEntry: boolean;
  public contacts: Contact[];
  public formSubmittingEvent: Subject<void>;

  private routeUtils: RouteUtils;

  constructor(
    private fb: FormBuilder,
    private healthAuthResource: HealthAuthorityResource,
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
      this.healthAuthResource.updatePrivacyOfficer(this.route.snapshot.params.haid, this.form.value)
        .subscribe(() => this.nextRouteAfterSubmit());
    }
  }

  public onBack(): void {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_VENDORS);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = new ContactFormState(this.fb, this.formUtilsService).form;
  }

  private initForm() {
    this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .subscribe(({ privacyOfficers }: HealthAuthority) => {
        if (privacyOfficers.length) {
          // Will only ever be a single privacy officer
          this.form.patchValue(privacyOfficers[0]);
        }
      });
  }

  private nextRouteAfterSubmit() {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_TECHNICAL_SUPPORTS);
  }

  private routeTo(routeSegment?: string) {
    const routePath = (this.isInitialEntry && routeSegment)
      ? routeSegment
      : AdjudicationRoutes.ORGANIZATION_INFORMATION;
    this.routeUtils.routeRelativeTo(routePath, { queryParamsHandling: 'preserve' });
  }
}
