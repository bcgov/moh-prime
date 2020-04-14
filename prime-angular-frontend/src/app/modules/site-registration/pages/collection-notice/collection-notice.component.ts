import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-collection-notice',
  templateUrl: './collection-notice.component.html',
  styleUrls: ['./collection-notice.component.scss']
})
export class CollectionNoticeComponent implements OnInit {
  public isProfileCompleted: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) { }

  public onAccept() {
    this.router.navigate([SiteRoutes.MULTIPLE_SITES], { relativeTo: this.route.parent });
  }

  public ngOnInit() { }
}
