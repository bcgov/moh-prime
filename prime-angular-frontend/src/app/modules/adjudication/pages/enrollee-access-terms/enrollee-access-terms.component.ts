import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material';

import { Subscription } from 'rxjs';

import { AccessTerm } from '@shared/models/access-term.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrollee-access-terms',
  templateUrl: './enrollee-access-terms.component.html',
  styleUrls: ['./enrollee-access-terms.component.scss']
})
export class EnrolleeAccessTermsComponent implements OnInit {
  public busy: Subscription;
  public dataSource: MatTableDataSource<AccessTerm>;

  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private route: ActivatedRoute,
    private adjudicationResource: AdjudicationResource
  ) { }

  public ngOnInit() {
    this.getAccessTerms();
  }

  private getAccessTerms() {
    const enrolleeId = this.route.snapshot.params.id;
    this.busy = this.adjudicationResource.getAccessTerms(enrolleeId)
      .subscribe((accessTerms: AccessTerm[]) =>
        this.dataSource = new MatTableDataSource<AccessTerm>(accessTerms)
      );
  }
}
