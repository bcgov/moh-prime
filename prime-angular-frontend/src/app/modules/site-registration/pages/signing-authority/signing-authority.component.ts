import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-signing-authority',
  templateUrl: './signing-authority.component.html',
  styleUrls: ['./signing-authority.component.scss']
})
export class SigningAuthorityComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.title = 'Signing Authority';
  }

  // TODO provide model when backend exists
  public onSubmit(data: { [key: string]: any }) {
    // TODO use ViewChild to get form value from child component when onSubmit invoked by page footer
    this.router.navigate([SiteRoutes.ADMINISTRATOR], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.HOURS_OPERATION], { relativeTo: this.route.parent });
  }

  public ngOnInit() { }
}
