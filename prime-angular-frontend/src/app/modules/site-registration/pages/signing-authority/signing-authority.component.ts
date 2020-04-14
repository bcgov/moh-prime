import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-signing-authority',
  templateUrl: './signing-authority.component.html',
  styleUrls: ['./signing-authority.component.scss']
})
export class SigningAuthorityComponent implements OnInit {
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
    this.router.navigate([SiteRoutes.ADMINISTRATOR], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.VENDOR], { relativeTo: this.route.parent });
  }

  public ngOnInit() { }
}
