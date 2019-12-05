import { Component, OnInit } from '@angular/core';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-collection-notice',
  templateUrl: './collection-notice.component.html',
  styleUrls: ['./collection-notice.component.scss']
})
export class CollectionNoticeComponent implements OnInit {
  public profileCompleted: boolean;

  constructor(
    private enrolmentService: EnrolmentService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  public EnrolmentRoutes = EnrolmentRoutes;

  public ngOnInit() {
    const enrolment = this.enrolmentService.enrolment;
    this.profileCompleted = enrolment.profileCompleted;
  }

  public onAccept() {
    const route = (!this.profileCompleted)
      ? EnrolmentRoutes.PROFILE
      : EnrolmentRoutes.REVIEW;

    this.router.navigate([route], { relativeTo: this.route.parent });
  }
}
