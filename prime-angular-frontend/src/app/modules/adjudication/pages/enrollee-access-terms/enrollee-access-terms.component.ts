import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatSelectChange } from '@angular/material';

import { Subscription } from 'rxjs';

import moment from 'moment';

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
  public accessTerms: AccessTerm[];
  public years: number[];
  public currentYear: number;

  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private route: ActivatedRoute,
    private adjudicationResource: AdjudicationResource
  ) {
    this.getYears();
  }

  public onChange({ value: year }: MatSelectChange) {
    this.getAccessTerms(year);
  }

  public ngOnInit() {
    this.getAccessTerms(this.currentYear);
  }

  private getAccessTerms(year: number = null) {
    const enrolleeId = this.route.snapshot.params.id;
    this.busy = this.adjudicationResource.getAccessTerms(enrolleeId, year)
      .subscribe((accessTerms: AccessTerm[]) => this.accessTerms = accessTerms);
  }

  private getYears() {
    const initialYear = 2020; // Deployed to production
    this.currentYear = +moment().format('YYYY');
    this.years = [];

    for (let i = this.currentYear; i >= initialYear; i--) {
      this.years.push(i);
    }
  }
}
