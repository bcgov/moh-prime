import { Component, OnInit } from '@angular/core';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { Router, ActivatedRoute, Params } from '@angular/router';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-collection-notice-alert',
  templateUrl: './collection-notice-alert.component.html',
  styleUrls: ['./collection-notice-alert.component.scss']
})
export class CollectionNoticeAlertComponent implements OnInit {
  public showNotice: boolean;
  public profileCompleted: boolean;

  constructor(
    private enrolmentService: EnrolmentService,
    private router: Router,
    private route: ActivatedRoute
  ) { }

  public EnrolmentRoutes = EnrolmentRoutes;

  public ngOnInit() {
    this.showNotice = !!this.enrolmentService.enrolment.collectionNoticeAccepted;
    const enrolment = this.enrolmentService.enrolment;
    this.profileCompleted = (enrolment) ? enrolment.profileCompleted : false;
  }

  public show() {
    return this.showNotice;
  }

  public onAccept() {
    const route = (!this.profileCompleted)
      ? EnrolmentRoutes.DEMOGRAPHIC
      : EnrolmentRoutes.OVERVIEW;

    this.router.navigate([route], { relativeTo: this.route.parent });
    this.enrolmentService.enrolment.collectionNoticeAccepted = true;
    this.showNotice = false;
  }
}
