import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { AbstractComponent } from '@shared/classes/abstract-component';
import { HttpEnrollee } from '@shared/models/enrolment.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrolment',
  templateUrl: './enrolment.component.html',
  styleUrls: ['./enrolment.component.scss']
})
export class EnrolmentComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  public enrollee: HttpEnrollee;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private adjudicationResource: AdjudicationResource
  ) {
    super(route, router);
  }

  public routeTo() {
    const routePath = AdjudicationRoutes.routePath(AdjudicationRoutes.ENROLMENTS);
    super.routeTo(routePath);
  }

  public ngOnInit() {
    this.getEnrollee(this.route.snapshot.params.id);
  }

  private getEnrollee(enrolleeId: number, statusCode?: number) {
    this.busy = this.adjudicationResource.getEnrolleeById(enrolleeId, statusCode)
      .subscribe((enrollee: HttpEnrollee) => this.enrollee = enrollee);
  }
}
