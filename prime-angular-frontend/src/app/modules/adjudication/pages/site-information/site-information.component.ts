import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { SiteResource } from '@core/resources/site-resource.service';
import { AbstractComponent } from '@shared/classes/abstract-component';
import { Site } from '@registration/shared/models/site.model';

@Component({
  selector: 'app-site-information',
  templateUrl: './site-information.component.html',
  styleUrls: ['./site-information.component.scss']
})
export class SiteInformationComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  public site: Site;
  public hasActions: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private siteResource: SiteResource
  ) {
    super(route, router);

    this.hasActions = false;
  }

  public ngOnInit(): void {
    this.getSite(this.route.snapshot.params.sid);
  }

  private getSite(siteId: number, statusCode?: number): void {
    this.busy = this.siteResource.getSiteById(siteId, statusCode)
      .subscribe((site: Site) => this.site = site);
  }
}
