import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AbstractComponent } from '@shared/classes/abstract-component';

@Component({
  selector: 'app-community-site-submission-page',
  templateUrl: './community-site-submission-page.component.html',
  styleUrls: ['./community-site-submission-page.component.scss']
})
export class CommunitySiteSubmissionPageComponent extends AbstractComponent implements OnInit {
  public hasActions: boolean;

  public siteId: number;
  public siteSubmissionId: number;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
  ) {
    super(route, router);
    this.hasActions = false;
  }

  ngOnInit(): void {
    this.siteId = +this.route.snapshot.params.sid;
    this.siteSubmissionId = +this.route.snapshot.params.ssid;
  }
}
