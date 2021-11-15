import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { SiteResource } from '@core/resources/site-resource.service';

import { AbstractSiteAdminPage } from '@adjudication/shared/classes/abstract-site-admin-page.class';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Site } from '@registration/shared/models/site.model';
import { HealthAuthoritySiteAdminList } from '@health-auth/shared/models/health-authority-admin-site-list.model';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';

@Component({
  selector: 'app-health-authority-site-container',
  templateUrl: './health-authority-site-container.component.html',
  styleUrls: ['./health-authority-site-container.component.scss']
})
export class HealthAuthoritySiteContainerComponent extends AbstractSiteAdminPage implements OnInit {
  @Input() public content: TemplateRef<any>;
  @Input() public actions: TemplateRef<any>;
  @Input() public hasActions: boolean;

  public healthAuthoritySite: HealthAuthoritySiteAdminList;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected siteResource: SiteResource,
    protected adjudicationResource: AdjudicationResource,
    protected healthAuthSiteResource: HealthAuthoritySiteResource
  ) {
    super(route, router, dialog, siteResource, adjudicationResource, healthAuthSiteResource);

    this.hasActions = false;
  }

  ngOnInit(): void {
    this.getDataset();
  }

  protected getDataset(): void {
    this.busy = this.healthAuthSiteResource
      .getHealthAuthorityAdminSites(this.route.snapshot.params.haid, this.route.snapshot.params.sid)
      .subscribe((sites: HealthAuthoritySiteAdminList[]) => this.healthAuthoritySite = sites[0]);
  }
  protected updateSite(siteId: number, updatedSiteFields: {}): void {
    // TODO: fix updated site shit
    // const updateHASite = {
    //   ...this.healthAuthoritySite,
    //   ...updatedSite
    // };
    // this.healthAuthoritySite = updateHASite;
  }

}
