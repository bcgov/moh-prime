import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { SiteRoutes } from 'app/modules/site-registration/site-registration.routes';
import { CollectionNoticeComponent } from '@enrolment/pages/collection-notice/collection-notice.component';
import { SiteCollectionNoticeComponent } from 'app/modules/site-registration/pages/site-collection-notice/site-collection-notice.component';

@Component({
  selector: 'app-collection-notice-alert',
  templateUrl: './collection-notice-alert.component.html',
  styleUrls: ['./collection-notice-alert.component.scss']
})
export class CollectionNoticeAlertComponent implements OnInit {
  public isProfileCompleted: boolean;
  public EnrolmentRoutes = EnrolmentRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private enrolmentService: EnrolmentService
  ) { }

  public get label(): string {
    return (!this.isProfileCompleted)
      ? 'Next'
      : 'Ok';
  }

  public show(): boolean {
    return this.authService.hasJustLoggedIn;
  }

  public onAccept() {
    this.authService.hasJustLoggedIn = false;

    const route = (!this.isProfileCompleted)
      ? EnrolmentRoutes.DEMOGRAPHIC
      : EnrolmentRoutes.OVERVIEW;

    if (this.enrolmentService.isInitialEnrolment) {
      this.router.navigate([route], { relativeTo: this.route.parent });
    }
  }

  public ngOnInit() {
    this.isProfileCompleted = this.enrolmentService.isProfileComplete;
  }
}
