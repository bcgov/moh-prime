import { Component, OnInit } from '@angular/core';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { Router, ActivatedRoute } from '@angular/router';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { AuthService } from '@auth/shared/services/auth.service';

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
    private enrolmentService: EnrolmentService,
  ) { }

  public get label(): string {
    return (!this.isProfileCompleted)
      ? 'Next'
      : 'Ok';
  }

  public show() {
    return this.authService.hasJustLoggedIn;
  }

  public onAccept() {
    const route = (!this.isProfileCompleted)
      ? EnrolmentRoutes.DEMOGRAPHIC
      : EnrolmentRoutes.OVERVIEW;

    this.authService.hasJustLoggedIn = false;

    if (this.enrolmentService.isInitialEnrolment) {
      this.router.navigate([route], { relativeTo: this.route.parent });
    }
  }

  public ngOnInit() {
    this.isProfileCompleted = this.enrolmentService.isProfileComplete;
  }
}
