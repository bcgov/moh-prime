import { Component, OnInit } from '@angular/core';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Enrolment } from '@shared/models/enrolment.model';

@Component({
  selector: 'app-collection-notice',
  templateUrl: './collection-notice.component.html',
  styleUrls: ['./collection-notice.component.scss']
})
export class CollectionNoticeComponent implements OnInit {

  public profileCompleted: boolean;
  public enrolment: Enrolment;

  constructor(
    private authService: AuthService,
    private enrolmentService: EnrolmentService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  public ngOnInit() {
    this.enrolment = this.enrolmentService.enrolment;
    this.profileCompleted = (this.enrolment) ? this.enrolment.profileCompleted : false;
    this.authService.setHasJustLoggedIn(true);

    if (this.profileCompleted) {
      this.router.navigate([EnrolmentRoutes.OVERVIEW], { relativeTo: this.route.parent });
    }
  }
}
