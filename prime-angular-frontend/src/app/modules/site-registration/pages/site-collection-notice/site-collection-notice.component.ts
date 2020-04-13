import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-site-collection-notice',
  templateUrl: './site-collection-notice.component.html',
  styleUrls: ['./site-collection-notice.component.scss']
})
export class SiteCollectionNoticeComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { }

  public onAccept() {
    this.router.navigate([SiteRoutes.MULTIPLE_SITES], { relativeTo: this.route.parent });
  }

  public ngOnInit() { }
}
