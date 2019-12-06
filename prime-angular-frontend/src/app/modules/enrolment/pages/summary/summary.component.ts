import { Component, OnInit, Inject } from '@angular/core';

import { Subscription, Observable, forkJoin } from 'rxjs';
import { map } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { EnrolmentCertificateAccessToken } from '@shared/models/enrolment-certificate-access-token.model';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.scss']
})
export class SummaryComponent implements OnInit {
  public busy: Subscription;
  public enrolment: Enrolment;
  public tokens: EnrolmentCertificateAccessToken[];

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public generateProvisionerLink() {
    this.enrolmentResource.createEnrolmentCertificateAccessToken()
      .subscribe((token) => this.tokens.push(token));
  }

  public generateTokenUrl(tokenId: string): string {
    return `${this.config.loginRedirectUrl}/enrolment-certificate/${tokenId}`;
  }

  public ngOnInit() {
    this.enrolment = this.enrolmentService.enrolment;

    this.busy = this.enrolmentResource.enrolmentCertificateAccessTokens()
      .subscribe(
        (tokens: EnrolmentCertificateAccessToken[]) => this.tokens = tokens,
        (error: any) => {
          this.toastService.openErrorToast('Access tokens could be found.');
          this.logger.error('[EnrolmentCertificate]Summary::OnInitAccess error has occurred: ', error);
        });
  }
}
