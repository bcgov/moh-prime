import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { AbstractComponent } from '@shared/classes/abstract-component';
import { EnrolleeAgreement } from '@shared/models/agreement.model';

import { AdjudicationResource } from '@core/resources/adjudication-resource.service';
import { EnrolmentResource } from '@core/resources/enrolment-resource.service';

@Component({
  selector: 'app-enrollee-access-term',
  templateUrl: './enrollee-access-term.component.html',
  styleUrls: ['./enrollee-access-term.component.scss'],
  standalone: false
})
export class EnrolleeAccessTermComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  public accessTerm: EnrolleeAgreement;

  constructor(
    protected router: Router,
    protected route: ActivatedRoute,
    private enrolmentResource: EnrolmentResource
  ) {
    super(route, router);
  }

  public ngOnInit() {
    this.getAccessTerm();
  }

  private getAccessTerm() {
    const enrolleeId = this.route.snapshot.params.id;
    const accessTermId = this.route.snapshot.params.aid;
    this.busy = this.enrolmentResource.getAccessTerm(enrolleeId, accessTermId)
      .subscribe((accessTerm: EnrolleeAgreement) => this.accessTerm = accessTerm);
  }
}
