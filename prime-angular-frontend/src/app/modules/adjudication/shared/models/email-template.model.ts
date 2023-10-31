export interface EmailTemplate {
  id: number;
  template: string;
  templateName: string;
  modifiedDate?: string;
  subject: string;
  description: string;
  recipient: string;
}
