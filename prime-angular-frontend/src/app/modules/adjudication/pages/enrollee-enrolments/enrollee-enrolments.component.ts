import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSelectChange } from '@angular/material/select';

import { Subscription } from 'rxjs';

import moment from 'moment';

import { AccessTerm } from '@shared/models/access-term.model';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrollee-enrolments',
  templateUrl: './enrollee-enrolments.component.html',
  styleUrls: ['./enrollee-enrolments.component.scss']
})
export class EnrolleeEnrolmentsComponent implements OnInit {
  public busy: Subscription;
  public accessTerms: AccessTerm[];
  public years: number[];
  public selectedYear: number;
  public hasActions: boolean;

  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private adjudicationResource: AdjudicationResource
  ) {
    this.getYears();
    this.hasActions = true;
  }

  public isUnderAdjudication(enrollee: HttpEnrollee): boolean {
    return [EnrolmentStatus.UNDER_REVIEW, EnrolmentStatus.REQUIRES_TOA]
      .includes(enrollee?.currentStatus.statusCode);
  }

  public onAction() {
    this.getAccessTerms(this.selectedYear);
  }

  public onChange({ value: year }: MatSelectChange) {
    this.setQueryParams({ year });
    this.getAccessTerms(year);
  }

  public ngOnInit() {
    this.selectedYear = this.route.snapshot.queryParams.year || this.getCurrentYear();
    this.getAccessTerms(this.selectedYear);
  }

  private getAccessTerms(year: number = null) {
    const enrolleeId = this.route.snapshot.params.id;
    this.busy = this.adjudicationResource.getAccessTerms(enrolleeId, year)
      .subscribe((accessTerms: AccessTerm[]) => this.accessTerms = accessTerms);
  }

  private getYears() {
    const initialYear = 2020; // Year deployed to production
    const currentYear = this.getCurrentYear();
    this.years = [];

    for (let i = currentYear; i >= initialYear; i--) {
      this.years.push(i);
    }
  }

  private setQueryParams(queryParams: { year?: number } = { year: null }) {
    // Passing `null` removes the query parameter from the URL
    queryParams = { ...this.route.snapshot.queryParams, ...queryParams };
    this.router.navigate([], { queryParams });
  }

  private resetQueryParams() {
    this.setQueryParams();
  }

  private getCurrentYear(): number {
    return +moment().format('YYYY');
  }
}
