import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Contact } from '@lib/models/contact.model';
import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

import { TechnicalSupportForm } from '@health-auth/pages/technical-support-page/technical-support-form.model';

@Component({
  selector: 'app-technical-support-overview',
  template: `
    <app-overview-section title="Technical Support"
                          [showEditRedirect]="showEditRedirect"
                          [editRoute]="HealthAuthSiteRegRoutes.TECHNICAL_SUPPORT"
                          (route)="onRoute($event)">
      {{ technicalSupportName ? (technicalSupportName | default) : (technicalSupportContact | fullname) }}
    </app-overview-section>
  `,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class TechnicalSupportOverviewComponent extends AbstractOverview implements OnInit {
  @Input() technicalSupport: TechnicalSupportForm;
  @Input() technicalSupports: Contact[];

  @Input() technicalSupportName: string;
  public HealthAuthSiteRegRoutes = HealthAuthSiteRegRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public get technicalSupportContact(): Contact {
    return this.technicalSupports?.find(pa => pa.id === this.technicalSupport.healthAuthorityTechnicalSupportId) ?? null;
  }

  public ngOnInit(): void { }
}
