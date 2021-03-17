import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

@Component({
  selector: 'app-health-auth-site-reg-login-page',
  templateUrl: './health-auth-site-reg-login-page.component.html',
  styleUrls: ['./health-auth-site-reg-login-page.component.scss']
})
export class HealthAuthSiteRegLoginPageComponent implements OnInit {
  public title: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.title = route.snapshot.data.title;
  }

  public onLogin() {
    this.router.navigate([HealthAuthSiteRegRoutes.MODULE_PATH]);
  }

  public ngOnInit(): void { }
}
