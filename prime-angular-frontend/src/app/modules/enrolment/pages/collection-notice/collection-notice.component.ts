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
  public hasInitialStatus: boolean;
  constructor(
    private enrolmentService: EnrolmentService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  public EnrolmentRoutes = EnrolmentRoutes;

  public ngOnInit() {
    const enrolment = this.enrolmentService.enrolment;
    this.hasInitialStatus = (enrolment) ? enrolment.initialStatus : true;

  }

  public onClick() {

    const route = this.hasInitialStatus ? EnrolmentRoutes.PROFILE : EnrolmentRoutes.REVIEW;
    this.router.navigate([route], { relativeTo: this.route.parent });
  }
}
