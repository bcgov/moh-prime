import { Injectable } from '@angular/core';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { ToastService } from '@core/services/toast.service';
import { Observable } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { EmailTemplate } from '../models/email-template.model';

@Injectable({
  providedIn: 'root'
})
export class EmailTemplateResourceService {

  constructor(
    private apiResource: ApiResource,
    private toastService: ToastService,
    private logger: ConsoleLoggerService
  ) { }

  public getEmailTemplates(): Observable<EmailTemplate[]> {
    return this.apiResource.get<EmailTemplate[]>(`emails/management/templates`)
      .pipe(
        map((response: ApiHttpResponse<EmailTemplate[]>) => response.result),
        tap((templates: EmailTemplate[]) => this.logger.info('EMAIL_TEMPLATES', templates)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Email Templates could not be retrieved');
          this.logger.error('[Adjudication] EmailTemplateResource::getEmailTemplates error has occurred: ', error);
          throw error;
        })
      );
  }

  public getEmailTemplate(id: number): Observable<EmailTemplate> {
    return this.apiResource.get<EmailTemplate>(`emails/management/templates/${id}`)
      .pipe(
        map((response: ApiHttpResponse<EmailTemplate>) => response.result),
        tap((template: EmailTemplate) => this.logger.info('EMAIL_TEMPLATE', template)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Email Template could not be retrieved');
          this.logger.error('[Adjudication] EmailTemplateResource::getEmailTemplate error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateEmailTemplate(id: number, template: string): Observable<EmailTemplate> {
    const payload = { data: template };
    return this.apiResource.put<EmailTemplate>(`emails/management/templates/${id}`, payload)
      .pipe(
        map((response: ApiHttpResponse<EmailTemplate>) => response.result),
        tap(() => this.toastService.openSuccessToast('Email Template has been updated.')),
        tap((emailTemplate: EmailTemplate) => this.logger.info('EMAIL_TEMPLATE', emailTemplate)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Email Template could not be updated');
          this.logger.error('[Adjudication] EmailTemplateResource::updateEmailTemplate error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateEmailSubject(id: number, subject: string): Observable<EmailTemplate> {
    const payload = { data: subject };
    return this.apiResource.put<EmailTemplate>(`emails/management/subject/${id}`, payload)
      .pipe(
        map((response: ApiHttpResponse<EmailTemplate>) => response.result),
        tap(() => this.toastService.openSuccessToast('Email Subject has been updated.')),
        tap((emailTemplate: EmailTemplate) => this.logger.info('EMAIL_SUBJECT', emailTemplate)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Email Subject could not be updated');
          this.logger.error('[Adjudication] EmailTemplateResource::updateEmailSubject error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateEmailTitle(id: number, title: string): Observable<EmailTemplate> {
    const payload = { data: title };
    return this.apiResource.put<EmailTemplate>(`emails/management/title/${id}`, payload)
      .pipe(
        map((response: ApiHttpResponse<EmailTemplate>) => response.result),
        tap(() => this.toastService.openSuccessToast('Email Title has been updated.')),
        tap((emailTemplate: EmailTemplate) => this.logger.info('EMAIL_TITLE', emailTemplate)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Email Title could not be updated');
          this.logger.error('[Adjudication] EmailTemplateResource::updateEmailTitle error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateEmailDescription(id: number, subject: string): Observable<EmailTemplate> {
    const payload = { data: subject };
    return this.apiResource.put<EmailTemplate>(`emails/management/description/${id}`, payload)
      .pipe(
        map((response: ApiHttpResponse<EmailTemplate>) => response.result),
        tap(() => this.toastService.openSuccessToast('Email Subject has been updated.')),
        tap((emailTemplate: EmailTemplate) => this.logger.info('EMAIL_SUBJECT', emailTemplate)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Email Subject could not be updated');
          this.logger.error('[Adjudication] EmailTemplateResource::updateEmailSubject error has occurred: ', error);
          throw error;
        })
      );
  }

  public updateEmailRecipient(id: number, recipient: string): Observable<EmailTemplate> {
    const payload = { data: recipient };
    return this.apiResource.put<EmailTemplate>(`emails/management/recipient/${id}`, payload)
      .pipe(
        map((response: ApiHttpResponse<EmailTemplate>) => response.result),
        tap(() => this.toastService.openSuccessToast('Email Recipient has been updated.')),
        tap((emailTemplate: EmailTemplate) => this.logger.info('EMAIL_RECIPIENT', emailTemplate)),
        catchError((error: any) => {
          this.toastService.openErrorToast('Email Recipient could not be updated');
          this.logger.error('[Adjudication] EmailTemplateResource::updateEmailRecipient error has occurred: ', error);
          throw error;
        })
      );
  }
}
