import { Component, OnInit } from '@angular/core';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { Router, ActivatedRoute } from '@angular/router';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { Enrolment } from '@shared/models/enrolment.model';

@Component({
  selector: 'app-collection-notice-alert',
  templateUrl: './collection-notice-alert.component.html',
  styleUrls: ['./collection-notice-alert.component.scss']
})
export class CollectionNoticeAlertComponent implements OnInit {
  public profileCompleted: boolean;
  public enrolment: Enrolment;

  constructor(
    private authService: AuthService,
    private enrolmentService: EnrolmentService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  public EnrolmentRoutes = EnrolmentRoutes;

  public get buttonText(): string {
    return (!this.profileCompleted)
      ? 'Next'
      : 'Ok';
  }

  public ngOnInit() {
    this.enrolment = this.enrolmentService.enrolment;
    this.profileCompleted = (this.enrolment) ? this.enrolment.profileCompleted : false;
  }

  public show() {
    return this.authService.hasJustLoggedIn;
  }

  public onAccept() {
    const route = (!this.profileCompleted)
      ? EnrolmentRoutes.DEMOGRAPHIC
      : EnrolmentRoutes.OVERVIEW;

    this.authService.hasJustLoggedIn = false;

    this.router.navigate([route], { relativeTo: this.route.parent });
  }
}
