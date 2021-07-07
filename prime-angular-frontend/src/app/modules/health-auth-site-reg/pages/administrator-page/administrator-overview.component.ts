import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

import { AdministratorForm } from './administrator-form.model';

@Component({
  selector: 'app-administrator-overview',
  template: `
    <app-overview-section title="PharmaNet Administrator"
                          [showEditRedirect]="showEditRedirect"
                          [editRoute]="HealthAuthSiteRegRoutes.ADMINISTRATOR"
                          (route)="onRoute($event)">
      <app-party-review [party]="administrator"></app-party-review>
    </app-overview-section>
  `,
  styles: [],
  encapsulation: ViewEncapsulation.None
})
export class AdministratorOverviewComponent extends AbstractOverview implements OnInit {
  @Input() administrator: AdministratorForm;
  public HealthAuthSiteRegRoutes = HealthAuthSiteRegRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public ngOnInit(): void { }
}
