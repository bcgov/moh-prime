import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Contact } from '@lib/models/contact.model';
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
      {{ pharmanetAdministrator | fullname }}
    </app-overview-section>
  `,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AdministratorOverviewComponent extends AbstractOverview implements OnInit {
  @Input() administrator: AdministratorForm;
  @Input() pharmanetAdministrators: Contact[];
  public HealthAuthSiteRegRoutes = HealthAuthSiteRegRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public get pharmanetAdministrator(): Contact {
    return this.pharmanetAdministrators
      ?.find(pa => pa.id === this.administrator.healthAuthorityPharmanetAdministratorId) ?? null;
  }

  public ngOnInit(): void { }
}
