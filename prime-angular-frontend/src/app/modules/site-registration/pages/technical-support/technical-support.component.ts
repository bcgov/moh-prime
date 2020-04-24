import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-technical-support',
  templateUrl: './technical-support.component.html',
  styleUrls: ['./technical-support.component.scss']
})
export class TechnicalSupportComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.title = 'Technical Support';
  }

  // TODO provide model when backend exists
  public onSubmit(data: { [key: string]: any }) {
    // TODO use ViewChild to get form value from child component when onSubmit invoked by page footer
    this.router.navigate([SiteRoutes.SITE_REVIEW], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.PRIVACY_OFFICER], { relativeTo: this.route.parent });
  }

  public ngOnInit() { }
}
