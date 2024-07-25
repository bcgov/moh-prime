import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

@Component({
  selector: 'app-ha-site-submission-list-page',
  templateUrl: './ha-site-submission-list-page.component.html',
  styleUrls: ['./ha-site-submission-list-page.component.scss']
})
export class HaSiteSubmissionListPageComponent implements OnInit {
  public hasActions: boolean;
  public siteId: number;
  public currentSubmissionId: number;

  constructor(
    private route: ActivatedRoute,
    private haResource: HealthAuthorityResource,
  ) {
    this.hasActions = false;
  }

  ngOnInit(): void {
    this.siteId = +this.route.snapshot.params.sid;
    this.haResource.getHealthAuthorityAdminSite(+this.route.snapshot.params.haid, +this.route.snapshot.params.sid)
      .subscribe(site => this.currentSubmissionId = site.currentSubmission?.id);
  }

}
