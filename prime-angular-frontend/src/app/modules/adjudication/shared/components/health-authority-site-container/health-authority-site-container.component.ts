import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';

import { AbstractSiteAdminPage } from '@adjudication/shared/classes/abstract-site-admin-page.class';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Site } from '@registration/shared/models/site.model';
import { HealthAuthoritySiteListItem } from '@health-auth/shared/models/health-authority-site-list.model';

@Component({
  selector: 'app-health-authority-site-container',
  templateUrl: './health-authority-site-container.component.html',
  styleUrls: ['./health-authority-site-container.component.scss']
})
export class HealthAuthoritySiteContainerComponent extends AbstractSiteAdminPage implements OnInit {
  public healthAuthoritySites: HealthAuthoritySiteListItem[];

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected siteResource: SiteResource,
    protected adjudicationResource: AdjudicationResource,
    protected healthAuthResource: HealthAuthorityResource,
  ) {
    super(route, router, dialog, siteResource, adjudicationResource, healthAuthResource);
  }

  ngOnInit(): void {
    this.getDataset();
  }

  protected getDataset(): void {
    this.busy = this.healthAuthResource
      .getHealthAuthoritySites(this.route.snapshot.params.haid, this.route.snapshot.params.sid)
      .subscribe((sites: HealthAuthoritySiteListItem[]) => this.healthAuthoritySites = sites);
  }
  protected updateSite(updatedSite: Site): void {
    throw new Error('Method not implemented.');
  }

}
