import { Component, OnInit } from '@angular/core';
import { Subscription, EMPTY } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { SiteRoutes } from '../../site-registration.routes';

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

  public onRoute(routePath: EnrolmentRoutes) {
    this.router.navigate([routePath], { relativeTo: this.route.parent });
  }

  public ngOnInit() { }
}
