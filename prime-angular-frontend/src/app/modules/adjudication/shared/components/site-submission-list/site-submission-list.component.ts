import moment from 'moment';
import { Component, Input, OnInit } from '@angular/core';
import { MatSelectChange } from '@angular/material/select';
import { SiteResource } from '@core/resources/site-resource.service';
import { SiteSubmission } from '@shared/models/site-submission.model';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-site-submission-list',
  templateUrl: './site-submission-list.component.html',
  styleUrls: ['./site-submission-list.component.scss']
})
export class SiteSubmissionListComponent implements OnInit {
  @Input() public siteId: number;
  @Input() public currentSubmissionId: number;

  public years: number[];
  public selectedYear: number;
  public submissions: SiteSubmission[];

  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    private siteResource: SiteResource,
  ) {
    this.getYears();
  }

  ngOnInit(): void {
    this.selectedYear = this.getCurrentYear();
    this.refreshSubmissionList();
  }

  public onChange({ value: year }: MatSelectChange) {
    this.selectedYear = year;
    this.refreshSubmissionList();
  }

  public refreshSubmissionList() {
    this.siteResource.getSiteSubmissions(this.siteId)
      .subscribe(siteSubmissions => {
        this.submissions = siteSubmissions.filter(ss => +moment(ss.createdDate).format('YYYY') === this.selectedYear);
      });
  }


  private getYears(): void {
    const initialYear = 2020; // Year PRIME deployed to production
    const currentYear = this.getCurrentYear();
    this.years = [];

    for (let i = currentYear; i >= initialYear; i--) {
      this.years.push(i);
    }
  }

  private getCurrentYear(): number {
    return +moment().format('YYYY');
  }
}
