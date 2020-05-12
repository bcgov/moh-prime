import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { AbstractComponent } from '@shared/classes/abstract-component';
import { AccessTerm } from '@shared/models/access-term.model';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrollee-access-term',
  templateUrl: './enrollee-access-term.component.html',
  styleUrls: ['./enrollee-access-term.component.scss']
})
export class EnrolleeAccessTermComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  public accessTerm: AccessTerm;

  constructor(
    protected router: Router,
    protected route: ActivatedRoute,
    private adjudicationResource: AdjudicationResource
  ) {
    super(route, router);
  }

  public ngOnInit() {
    this.getAccessTerm();
  }

  private getAccessTerm() {
    const enrolleeId = this.route.snapshot.params.id;
    const accessTermId = this.route.snapshot.params.hid;
    this.busy = this.adjudicationResource.getAccessTerm(enrolleeId, accessTermId)
      .subscribe((accessTerm: AccessTerm) => this.accessTerm = accessTerm);
  }
}
