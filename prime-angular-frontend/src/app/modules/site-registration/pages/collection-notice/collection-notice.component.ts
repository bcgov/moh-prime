import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthService } from '@auth/shared/services/auth.service';
import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-collection-notice',
  templateUrl: './collection-notice.component.html',
  styleUrls: ['./collection-notice.component.scss']
})
export class CollectionNoticeComponent implements OnInit {
  public isFull: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService
  ) {
    this.isFull = true;
  }

  public onAccept() {
    this.authService.hasJustLoggedIn = false;
    this.router.navigate([SiteRoutes.MULTIPLE_SITES], { relativeTo: this.route.parent });
  }

  public ngOnInit() {
    this.authService.hasJustLoggedIn = true;
  }
}
