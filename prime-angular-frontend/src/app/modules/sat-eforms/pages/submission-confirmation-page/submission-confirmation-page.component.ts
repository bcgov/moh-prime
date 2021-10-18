import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { SatEformsRoutes } from '@sat/sat-eforms.routes';

@Component({
  selector: 'app-submission-confirmation-page',
  templateUrl: './submission-confirmation-page.component.html',
  styleUrls: ['./submission-confirmation-page.component.scss']
})
export class SubmissionConfirmationPageComponent implements OnInit {
  public title: string;
  public busy: Subscription;
  public routeUtils: RouteUtils;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SatEformsRoutes.MODULE_PATH);
  }

  public ngOnInit(): void { }
}
