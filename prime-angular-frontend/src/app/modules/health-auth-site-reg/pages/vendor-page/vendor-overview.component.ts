import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

import { VendorForm } from './vendor-form.model';

@Component({
  selector: 'app-vendor-overview',
  template: `
    <app-overview-section title="Vendor"
                          [showEditRedirect]="showEditRedirect"
                          [editRoute]="HealthAuthSiteRegRoutes.VENDOR"
                          (route)="onRoute($event)">
      <app-enrollee-property title="Vendor">
        {{ vendor?.vendorCode | configCode: 'vendors' | default }}
      </app-enrollee-property>
    </app-overview-section>
  `,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class VendorOverviewComponent extends AbstractOverview implements OnInit {
  @Input() public vendor: VendorForm;
  public HealthAuthSiteRegRoutes = HealthAuthSiteRegRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public ngOnInit(): void { }
}
