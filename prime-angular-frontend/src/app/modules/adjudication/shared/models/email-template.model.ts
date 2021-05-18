import { EmailTemplateTypeEnum } from './email-template-type.model';

export interface EmailTemplate {
  id: number;
  emailType: EmailTemplateTypeEnum;
  template: string;
  modifiedDate?: string;
}
