import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { EmailTemplate } from '@adjudication/shared/models/email-template.model';
import { EmailTemplateResourceService } from '@adjudication/shared/services/email-template-resource.service';

@Component({
  selector: 'app-email-notification-view-page',
  templateUrl: './email-notification-view-page.component.html',
  styleUrls: ['./email-notification-view-page.component.scss']
})
export class EmailNotificationViewPageComponent implements OnInit {
  public busy: Subscription;
  public template: EmailTemplate;

  private routeUtils: RouteUtils;

  constructor(
    private emailTemplateResource: EmailTemplateResourceService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.NOTIFICATION_EMAILS));
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.NOTIFICATION_EMAILS]);
  }

  public ngOnInit(): void {
    this.getEmailTemplate();
  }

  public getEmailTemplate() {
    //TODO: Future filter by url for either enrollee or site
    this.busy = this.emailTemplateResource.getEmailTemplate(this.route.snapshot.params.eid)
      .subscribe(template => this.template = template);
  }
}
