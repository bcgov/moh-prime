import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { BaseEnrolmentPage } from '@enrolment/shared/classes/enrolment-page.class';

@Component({
  selector: 'app-minor-update-confirmation',
  templateUrl: './minor-update-confirmation.component.html',
  styleUrls: ['./minor-update-confirmation.component.scss']
})
export class MinorUpdateConfirmationComponent extends BaseEnrolmentPage implements OnInit {
  constructor(
    protected route: ActivatedRoute,
    protected router: Router
  ) {
    super(route, router);
  }

  public ngOnInit(): void { }
}
