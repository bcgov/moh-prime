import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

import { SiteAddressForm } from './site-address-form.model';

// TODO address view shouldn't manage routing internally refactor to emit redirectRoutePath
@Component({
  selector: 'app-site-address-overview',
  template: `
    <app-address-view title="Site Address"
                      [address]="siteAddress?.physicalAddress"
                      [showRedirect]="showEditRedirect"
                      [showIfEmpty]="true"
                      (route)="onRoute(HealthAuthSiteRegRoutes.SITE_ADDRESS)">
    </app-address-view>
  `,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SiteAddressOverviewComponent extends AbstractOverview implements OnInit {
  @Input() public siteAddress: SiteAddressForm;
  public HealthAuthSiteRegRoutes = HealthAuthSiteRegRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public ngOnInit(): void { }
}
