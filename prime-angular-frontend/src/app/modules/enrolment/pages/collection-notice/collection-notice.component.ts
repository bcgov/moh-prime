import { Component, OnInit } from '@angular/core';

import { AuthService } from '@auth/shared/services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

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

    if (this.enrolmentService.isProfileComplete) {
      this.router.navigate([EnrolmentRoutes.OVERVIEW], { relativeTo: this.route.parent });
    }
  }
}
