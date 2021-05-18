import { EmailTemplateTypeEnum } from '@adjudication/shared/models/email-template-type.model';
import { EmailTemplate } from '@adjudication/shared/models/email-template.model';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-notification-email-view',
  templateUrl: './notification-email-view.component.html',
  styleUrls: ['./notification-email-view.component.scss']
})
export class NotificationEmailViewComponent implements OnInit {
  @Input() public emailTemplate: EmailTemplate

  public EmailTemplateTypeEnum = EmailTemplateTypeEnum;

  constructor(
  ) { }

  ngOnInit(): void {
  }

}
