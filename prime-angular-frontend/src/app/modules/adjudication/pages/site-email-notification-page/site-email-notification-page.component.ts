import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { EmailTemplate } from '@adjudication/shared/models/email-template.model';
import { EmailTemplateResourceService } from '@adjudication/shared/services/email-template-resource.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-site-email-notification-page',
  templateUrl: './site-email-notification-page.component.html',
  styleUrls: ['./site-email-notification-page.component.scss']
})
export class SiteEmailNotificationPageComponent implements OnInit {
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

  public onBack() {
    this.routeUtils.routeRelativeTo(['../']);
  }

  ngOnInit(): void {
    this.getEmailTemplate();
  }

  public getEmailTemplate() {
    this.busy = this.emailTemplateResource.getEmailTemplate(this.route.snapshot.params.eid).subscribe(template => this.template = template);
  }
}
