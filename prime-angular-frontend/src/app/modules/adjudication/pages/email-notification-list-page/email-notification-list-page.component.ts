import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { EmailTemplate } from '@adjudication/shared/models/email-template.model';
import { EmailTemplateResourceService } from '@adjudication/shared/services/email-template-resource.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-email-notification-list-page',
  templateUrl: './email-notification-list-page.component.html',
  styleUrls: ['./email-notification-list-page.component.scss']
})
export class EmailNotificationListPageComponent implements OnInit {
  public templates: EmailTemplate[];
  public busy: Subscription;

  private routeUtils: RouteUtils;

  constructor(
    private emailTemplateResource: EmailTemplateResourceService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.NOTIFICATION_EMAILS));
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(['../']);
  }

  public ngOnInit(): void {
    this.getEmailTemplates();
  }

  private getEmailTemplates() {
    //TODO: Future filter by url for either enrollee or site
    this.busy = this.emailTemplateResource.getEmailTemplates()
      .subscribe(templates => this.templates = templates);
  }
}
