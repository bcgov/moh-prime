import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

import { HealthAuthoritySiteAdmin } from '@health-auth/shared/models/health-authority-admin-site.model';

@Component({
  selector: 'app-site-overview-page',
  templateUrl: './site-overview-page.component.html',
  styleUrls: ['./site-overview-page.component.scss']
})
export class SiteOverviewPageComponent implements OnInit {
  public busy: Subscription;
  public site: HealthAuthoritySiteAdmin;

  constructor(
    private healthAuthorityResource: HealthAuthorityResource,
    private route: ActivatedRoute,
  ) { }

  public ngOnInit(): void {
    this.busy = this.healthAuthorityResource
      .getHealthAuthorityAdminSite(this.route.snapshot.params.haid, this.route.snapshot.params.sid)
      .subscribe((site: HealthAuthoritySiteAdmin) => this.site = site);
  }
}
