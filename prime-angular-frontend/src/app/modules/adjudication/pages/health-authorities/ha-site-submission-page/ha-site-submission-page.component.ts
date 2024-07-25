import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AbstractComponent } from '@shared/classes/abstract-component';

@Component({
  selector: 'app-ha-site-submission-page',
  templateUrl: './ha-site-submission-page.component.html',
  styleUrls: ['./ha-site-submission-page.component.scss']
})
export class HaSiteSubmissionPageComponent extends AbstractComponent implements OnInit {
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
