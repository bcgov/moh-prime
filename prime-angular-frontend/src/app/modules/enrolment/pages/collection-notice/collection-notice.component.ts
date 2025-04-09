import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';

import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

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
    private authService: AuthService,
    private enrolmentService: EnrolmentService,
    private enrolmentResource: EnrolmentResource,
  ) {
    this.isFull = true;
  }

  public onAccept() {
    this.authService.hasJustLoggedIn = false;

    switch (this.enrolmentService.enrolment?.currentStatus.statusCode) {
      case EnrolmentStatusEnum.UNDER_REVIEW:
        this.router.navigate([EnrolmentRoutes.SUBMISSION_CONFIRMATION], { relativeTo: this.route.parent });
        break;
      case EnrolmentStatusEnum.REQUIRES_TOA:
        if (this.enrolmentService.enrolment?.requireRedoSelfDeclaration) {
          this.enrolmentResource.returnToEditing(this.enrolmentService.enrolment.id).subscribe(() =>
            this.router.navigate([EnrolmentRoutes.OVERVIEW], { relativeTo: this.route.parent })
          );
        } else {
          this.router.navigate([EnrolmentRoutes.PENDING_ACCESS_TERM], { relativeTo: this.route.parent });
        }
        break;
      default:
        this.router.navigate([EnrolmentRoutes.OVERVIEW], { relativeTo: this.route.parent });
        break;
    }
  }

  public ngOnInit(): void {
    this.authService.hasJustLoggedIn = true;

    // Collection notice is the initial route after login, and used as a hub
    // for redirection to an appropriate view based on the enrolment
    switch (this.enrolmentService.enrolment?.currentStatus.statusCode) {
      case EnrolmentStatusEnum.UNDER_REVIEW:
        this.router.navigate([EnrolmentRoutes.SUBMISSION_CONFIRMATION], { relativeTo: this.route.parent });
        break;
      case EnrolmentStatusEnum.REQUIRES_TOA:
        this.router.navigate([EnrolmentRoutes.PENDING_ACCESS_TERM], { relativeTo: this.route.parent });
        break;
    }
  }
}
