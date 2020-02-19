import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

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
    const currentRoutePath = this.route.snapshot.routeConfig.path;

    this.authService.hasJustLoggedIn = false;

    if (currentRoutePath === EnrolmentRoutes.COLLECTION_NOTICE) {
      const route = (!this.isProfileCompleted)
        ? EnrolmentRoutes.DEMOGRAPHIC
        : EnrolmentRoutes.OVERVIEW;

      if (this.enrolmentService.isInitialEnrolment) {
        this.router.navigate([route], { relativeTo: this.route.parent });
      }
    }
  }

  public ngOnInit() {
    console.log('isProfileComplete', this.enrolmentService.isProfileComplete);

    this.isProfileCompleted = this.enrolmentService.isProfileComplete;
  }
}
