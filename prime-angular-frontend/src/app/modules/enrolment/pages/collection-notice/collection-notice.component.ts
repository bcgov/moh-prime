import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';

import { AuthenticationService } from '@auth/shared/services/authentication.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

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
    private authenticationService: AuthenticationService,
    private enrolmentService: EnrolmentService
  ) {
    this.isFull = true;
  }

  public onAccept() {
    this.authenticationService.hasJustLoggedIn = false;

    const route = (!this.enrolmentService.isProfileComplete)
      ? EnrolmentRoutes.DEMOGRAPHIC
      : EnrolmentRoutes.OVERVIEW;

    if (this.enrolmentService.isInitialEnrolment) {
      this.router.navigate([route], { relativeTo: this.route.parent });
    }
  }

  public ngOnInit() {
    this.authenticationService.hasJustLoggedIn = true;

    // Collection notice is the initial route after login, and used as a hub
    // for redirection to an appropriate view based on the enrolment
    switch (this.enrolmentService.enrolment.currentStatus.statusCode) {
      case EnrolmentStatus.UNDER_REVIEW:
        this.router.navigate([EnrolmentRoutes.SUBMISSION_CONFIRMATION], { relativeTo: this.route.parent });
        break;
      case EnrolmentStatus.REQUIRES_TOA:
        this.router.navigate([EnrolmentRoutes.PENDING_ACCESS_TERM], { relativeTo: this.route.parent });
        break;
      default: {
        // Default redirect when completed, otherwise allow the view render
        if (this.enrolmentService.isProfileComplete) {
          this.router.navigate([EnrolmentRoutes.OVERVIEW], { relativeTo: this.route.parent });
        }
      }
    }
  }
}
