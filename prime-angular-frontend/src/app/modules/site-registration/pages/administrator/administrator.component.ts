import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-administrator',
  templateUrl: './administrator.component.html',
  styleUrls: ['./administrator.component.scss']
})
export class AdministratorComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.title = 'Administrator of PharmaNet Onboarding';
  }

  // TODO provide model when backend exists
  public onSubmit(data: { [key: string]: any }) {
    // TODO use ViewChild to get form value from child component when onSubmit invoked by page footer
    this.router.navigate([SiteRoutes.PRIVACY_OFFICER], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.SIGNING_AUTHORITY], { relativeTo: this.route.parent });
  }

  public ngOnInit() { }
}
