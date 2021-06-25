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
  selector: 'app-technical-supports-page',
  templateUrl: './technical-supports-page.component.html',
  styleUrls: ['./technical-supports-page.component.scss']
})
export class TechnicalSupportsPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public formState: ContactFormState;
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
      // TODO will be updated to be multiple contacts
      const technicalSupports: Contact[] = [this.form.value];
      this.healthAuthResource.updateTechnicalSupports(this.route.snapshot.params.haid, technicalSupports)
        .subscribe(() => this.nextRouteAfterSubmit());
    }
  }

  public onBack(): void {
    this.routeTo(AdjudicationRoutes.HEALTH_AUTH_PRIVACY_OFFICER);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.formState = new ContactFormState(this.fb, this.formUtilsService);
    this.form = this.formState.form;
  }

  private initForm() {
    this.healthAuthResource.getHealthAuthorityById(this.route.snapshot.params.haid)
      .subscribe(({ technicalSupports }: HealthAuthority) => {
        if (technicalSupports.length) {
          this.form.patchValue(technicalSupports[0]);
        }
      });
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
