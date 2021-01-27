import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription, Observable, BehaviorSubject } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Site } from '@registration/shared/models/site.model';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-site-adjudication',
  templateUrl: './site-adjudication.component.html',
  styleUrls: ['./site-adjudication.component.scss']
})
export class SiteAdjudicationComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public hasActions: boolean;
  public refresh: BehaviorSubject<boolean>;
  public site: Site;
  public AdjudicationRoutes = AdjudicationRoutes;

  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService,
    private siteResource: SiteResource
  ) {
    this.hasActions = true;
    this.refresh = new BehaviorSubject<boolean>(null);
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const siteId = this.route.snapshot.params.sid;
      this.busy = this.siteResource
        .updatePecCode(siteId, this.form.value.pec)
        .subscribe(() => this.refresh.next(true));
    }
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
  }

  public ngOnInit(): void {
    this.createFormInstance();

    this.busy = this.getSite()
      .subscribe((site: Site) => {
        this.site = site;
        this.form.patchValue(site);
      });
  }

  private createFormInstance() {
    this.form = this.fb.group({
      pec: [
        '',
        [Validators.required]
      ]
    });
  }

  private getSite(): Observable<Site> {
    const siteId = this.route.snapshot.params.sid;
    return this.siteResource.getSiteById(siteId);
  }
}
