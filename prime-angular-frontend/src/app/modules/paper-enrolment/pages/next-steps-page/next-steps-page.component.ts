import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup } from '@angular/forms';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-next-steps',
  templateUrl: './next-steps-page.component.html',
  styleUrls: ['./next-steps-page.component.scss']
})
export class NextStepsPageComponent implements OnInit {
  public form: FormGroup;
  public routeUtils: RouteUtils;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public onSubmit() {
    this.routeUtils.routeRelativeTo(['../', 0, PaperEnrolmentRoutes.DEMOGRAPHIC]);
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(['/', AdjudicationRoutes.MODULE_PATH]);
  }

  public ngOnInit(): void { }
}
