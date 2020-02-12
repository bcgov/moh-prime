import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Enrolment } from '@shared/models/enrolment.model';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-access-locked',
  templateUrl: './access-locked.component.html',
  styleUrls: ['./access-locked.component.scss']
})
export class AccessLocked extends BaseEnrolmentPage implements OnInit {
  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private enrolmentService: EnrolmentService
  ) {
    super(route, router);
  }

  public ngOnInit() {
    this.enrolmentService.enrolment$
      .subscribe((enrolment: Enrolment) =>
        this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment
      );
  }
}
