import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { UtilsService } from '@core/services/utils.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { EmailTemplateTypeEnum } from '@adjudication/shared/models/email-template-type.model';
import { EmailTemplate } from '@adjudication/shared/models/email-template.model';
import { EmailTemplateResourceService } from '@adjudication/shared/services/email-template-resource.service';


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
    private router: Router,
    private utilsService: UtilsService,
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.NOTIFICATION_EMAILS));
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(['../']);
  }

  public ngOnInit(): void {
    this.getEmailTemplates();
  }

  private getEmailTemplates(): void {
    this.busy = this.emailTemplateResource.getEmailTemplates()
      .subscribe(templates =>
        this.templates = templates
          .sort((a: EmailTemplate, b: EmailTemplate) =>
            EmailTemplateTypeEnum[a.emailType].localeCompare(EmailTemplateTypeEnum[b.emailType])
          )
      );
  }
}
