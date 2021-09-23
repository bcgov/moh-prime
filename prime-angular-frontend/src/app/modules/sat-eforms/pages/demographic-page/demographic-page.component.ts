import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { SatEformsRoutes } from '@sat/sat-eforms.routes';
import { SatEformsEnrollee } from '@sat/shared/models/sat-enrollee.model';

@Component({
  selector: 'app-demographic-page',
  templateUrl: './demographic-page.component.html',
  styleUrls: ['./demographic-page.component.scss']
})
export class DemographicPageComponent implements OnInit { // , AbstractEnrolmentPage {
  public title: string;
  public busy: Subscription;
  public form: FormGroup;
  public routeUtils: RouteUtils;

  public enrollee: SatEformsEnrollee;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SatEformsRoutes.MODULE_PATH);
  }

  public onSubmit() {
    this.routeUtils.routeRelativeTo(SatEformsRoutes.REGULATORY);
  }

  public ngOnInit(): void {
    this.form = new FormGroup({});
  }
}
