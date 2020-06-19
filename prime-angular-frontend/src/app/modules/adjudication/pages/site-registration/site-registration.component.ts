import { Component, OnInit } from '@angular/core';
import { Site } from '@registration/shared/models/site.model';
import { Subscription } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AbstractComponent } from '@shared/classes/abstract-component';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-site-registration',
  templateUrl: './site-registration.component.html',
  styleUrls: ['./site-registration.component.scss']
})
export class SiteRegistrationComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  public site: Site;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private adjudicationResource: AdjudicationResource
  ) {
    super(route, router);
  }

  public ngOnInit(): void {
    this.getSite(this.route.snapshot.params.id);
  }

  private getSite(siteId: number, statusCode?: number) {
    this.busy = this.adjudicationResource.getSiteById(siteId, statusCode)
      .pipe(
        map((site: Site) => site)
      )
      .subscribe((site: Site) => this.site = site);
  }
}
