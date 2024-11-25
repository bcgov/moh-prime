import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SiteResource } from '@core/resources/site-resource.service';

@Component({
  selector: 'app-community-site-submission-list-page',
  templateUrl: './community-site-submission-list-page.component.html',
  styleUrls: ['./community-site-submission-list-page.component.scss']
})
export class CommunitySiteSubmissionListPageComponent implements OnInit {
  public hasActions: boolean;
  public siteId: number;
  public currentSubmissionId: number;

  constructor(
    private route: ActivatedRoute,
    private siteResource: SiteResource,
  ) {
    this.hasActions = false;
  }

  ngOnInit(): void {
    this.siteId = +this.route.snapshot.params.sid;
    this.siteResource.getSiteById(this.siteId)
      .subscribe(site => this.currentSubmissionId = site.currentSubmission?.id);
  }

}
