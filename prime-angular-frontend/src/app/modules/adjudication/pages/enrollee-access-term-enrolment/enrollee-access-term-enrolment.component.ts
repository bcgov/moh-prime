import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { AbstractComponent } from '@shared/classes/abstract-component';
import { HttpEnrolleeProfileVersion } from '@shared/models/enrollee-profile-history.model';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrollee-access-term-enrolment',
  templateUrl: './enrollee-access-term-enrolment.component.html',
  styleUrls: ['./enrollee-access-term-enrolment.component.scss']
})
export class EnrolleeAccessTermEnrolmentComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  public enrolmentProfileHistory: HttpEnrolleeProfileVersion;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private adjudicationResource: AdjudicationResource,
  ) {
    super(route, router);
  }

  // TODO update to pass in route from template
  public routeTo() {
    super.routeTo('../');
  }

  public ngOnInit() {
    const enrolleeId = this.route.snapshot.params.id;
    const accessTermId = this.route.snapshot.params.hid;
    this.busy = this.adjudicationResource.getEnrolmentForAccessTerm(enrolleeId, accessTermId)
      .subscribe((enrolmentProfileVersion: HttpEnrolleeProfileVersion) =>
        this.enrolmentProfileHistory = enrolmentProfileVersion
      );
  }
}
