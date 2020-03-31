import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-site-review',
  templateUrl: './site-review.component.html',
  styleUrls: ['./site-review.component.scss']
})
export class SiteReviewComponent implements OnInit {
  public busy: Subscription;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { }

  public onSubmit() {
    this.router.navigate([SiteRoutes.CONFIRMATION], { relativeTo: this.route.parent });
  }

  public onRoute(routePath: SiteRoutes) {
    this.router.navigate([routePath], { relativeTo: this.route.parent });
  }

  public ngOnInit() { }
}
