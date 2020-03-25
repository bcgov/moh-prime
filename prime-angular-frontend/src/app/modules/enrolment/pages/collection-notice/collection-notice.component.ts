import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-collection-notice',
  templateUrl: './collection-notice.component.html',
  styleUrls: ['./collection-notice.component.scss']
})
export class CollectionNoticeComponent implements OnInit {
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private enrolmentService: EnrolmentService
  ) { }

  public ngOnInit() {
    this.authService.hasJustLoggedIn = true;

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
        if (this.enrolmentService.isProfileComplete) {
          this.router.navigate([EnrolmentRoutes.OVERVIEW], { relativeTo: this.route.parent });
        }
      }
    }
  }
}
