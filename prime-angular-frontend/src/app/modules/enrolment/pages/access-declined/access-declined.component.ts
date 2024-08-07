import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Enrolment } from '@shared/models/enrolment.model';

import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/enrolment-page.class';

@Component({
  selector: 'app-access-declined',
  templateUrl: './access-declined.component.html',
  styleUrls: ['./access-declined.component.scss']
})
export class AccessDeclinedComponent extends BaseEnrolmentPage implements OnInit {
  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private enrolmentService: EnrolmentService
  ) {
    super(route, router);
  }

  public ngOnInit(): void {
    this.enrolmentService.enrolment$
      .subscribe((enrolment: Enrolment) =>
        this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment
      );
  }
}
