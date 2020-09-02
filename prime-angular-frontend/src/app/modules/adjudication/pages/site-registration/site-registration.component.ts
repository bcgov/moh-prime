import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { AbstractComponent } from '@shared/classes/abstract-component';
import { Site } from '@registration/shared/models/site.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-site-registration',
  templateUrl: './site-registration.component.html',
  styleUrls: ['./site-registration.component.scss']
})
export class SiteRegistrationComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  public site: Site;
  public hasActions: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private adjudicationResource: AdjudicationResource
  ) {
    super(route, router);

    this.hasActions = false;
  }

  public ngOnInit(): void {
    this.getSite(this.route.snapshot.params.sid);
  }

  private getSite(siteId: number, statusCode?: number): void {
    this.busy = this.adjudicationResource.getSiteById(siteId, statusCode)
      .subscribe((site: Site) => this.site = site);
  }
}
