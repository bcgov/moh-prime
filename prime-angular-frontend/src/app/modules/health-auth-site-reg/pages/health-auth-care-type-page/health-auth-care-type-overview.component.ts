import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

import { HealthAuthCareTypeForm } from './health-auth-care-type-form.model';

@Component({
  selector: 'app-health-auth-care-type-overview',
  template: `
    <app-overview-section title="Health Authority Care Type"
                          [showEditRedirect]="showEditRedirect"
                          [editRoute]="HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_TYPE"
                          (route)="onRoute($event)">
      <app-enrollee-property title="Health Authority Care Type">
        {{ healthAuthCareType?.careType | default }}
      </app-enrollee-property>
    </app-overview-section>
  `,
  styles: ['mat-icon { font-size: 1.2em; }'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HealthAuthCareTypeOverviewComponent extends AbstractOverview {
  @Input() public healthAuthCareType: HealthAuthCareTypeForm;
  public HealthAuthSiteRegRoutes = HealthAuthSiteRegRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }
}
