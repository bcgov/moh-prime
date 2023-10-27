import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { EmailTemplate } from '@adjudication/shared/models/email-template.model';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';

@Component({
  selector: 'app-notification-emails-container',
  templateUrl: './notification-emails-container.component.html',
  styleUrls: ['./notification-emails-container.component.scss'],
  providers: [FormatDatePipe]
})
export class NotificationEmailsContainerComponent implements OnInit {
  @Input() public emailTemplates: EmailTemplate[];

  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formatDatePipe: FormatDatePipe,
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.LOGIN_PAGE));
  }

  public onView(id: number): void {
    this.routeUtils.routeRelativeTo([id]);
  }

  public getTemplateProperties(template: EmailTemplate) {
    return [
      {
        key: 'Subject',
        value: template.subject
      },
      {
        key: 'Last Modified',
        value: this.formatDatePipe.transform(template.modifiedDate)
      },
    ];
  }

  public ngOnInit(): void { }
}
