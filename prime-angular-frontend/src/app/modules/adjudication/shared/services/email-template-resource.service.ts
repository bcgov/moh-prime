import { Injectable } from '@angular/core';
import { ApiHttpResponse } from '@core/models/api-http-response.model';
import { ApiResource } from '@core/resources/api-resource.service';
import { LoggerService } from '@core/services/logger.service';
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
    private logger: LoggerService
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
}
