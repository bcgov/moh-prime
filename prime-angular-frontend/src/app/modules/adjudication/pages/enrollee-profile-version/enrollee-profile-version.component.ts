import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { AbstractComponent } from '@shared/classes/abstract-component';
import { HttpEnrolleeProfileVersion } from '@shared/models/enrollee-profile-history.model';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrollee-profile-version',
  templateUrl: './enrollee-profile-version.component.html',
  styleUrls: ['./enrollee-profile-version.component.scss']
})
export class EnrolleeProfileVersionComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  public enrolleeProfileVersion: HttpEnrolleeProfileVersion;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private adjudicationResource: AdjudicationResource
  ) {
    super(route, router);
  }

  public ngOnInit() {
    const enrolleeId = this.route.snapshot.params.id;
    const enrolleeProfileVersionId = this.route.snapshot.params.hid;
    this.busy = this.adjudicationResource
      .getEnrolleeProfileVersion(enrolleeId, enrolleeProfileVersionId)
      .subscribe((enrolleeProfileVersion: HttpEnrolleeProfileVersion) => this.enrolleeProfileVersion = enrolleeProfileVersion);
  }
}
