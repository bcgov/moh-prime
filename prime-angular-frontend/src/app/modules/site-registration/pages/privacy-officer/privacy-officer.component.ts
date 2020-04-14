import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-privacy-officer',
  templateUrl: './privacy-officer.component.html',
  styleUrls: ['./privacy-officer.component.scss']
})
export class PrivacyOfficerComponent implements OnInit {
  public title: string;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.title = 'Privacy Officer';
  }

  // TODO provide model when backend exists
  public onSubmit(data: { [key: string]: any }) {
    // TODO use ViewChild to get form value from child component when onSubmit invoked by page footer
    this.router.navigate([SiteRoutes.TECHNICAL_SUPPORT_CONTACT], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.ADMINISTRATOR], { relativeTo: this.route.parent });
  }

  public ngOnInit() { }
}
