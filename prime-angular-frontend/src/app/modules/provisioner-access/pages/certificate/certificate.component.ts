import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs';

import { EnrolmentCertificate } from '../../shared/models/enrolment-certificate.model';
import moment from 'moment';
import { ProvisionerAccessResource } from '../../shared/services/provisioner-access-resource.service';
import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';

@Component({
  selector: 'app-certificate',
  templateUrl: './certificate.component.html',
  styleUrls: ['./certificate.component.scss']
})
export class CertificateComponent implements OnInit {
  public busy: Subscription;
  public certificate: EnrolmentCertificate;
  public expiryDate: string;

  constructor(
    private route: ActivatedRoute,
    private enrolmentCertificateResource: ProvisionerAccessResource,
    private toastService: ToastService,
    private logger: ConsoleLoggerService
  ) { }

  public get hasPreferredName(): boolean {
    return (
      this.certificate &&
      (
        !!this.certificate.preferredFirstName ||
        !!this.certificate.preferredMiddleName ||
        !!this.certificate.preferredLastName
      )
    );
  }

  public get middleName(): string {
    return (this.hasPreferredName) ? this.certificate.preferredMiddleName : '';
  }

  public get fullName(): string {
    return !this.hasPreferredName
      ? `${this.certificate.firstName} ${this.certificate.lastName}`
      : `${this.certificate.preferredFirstName} ${this.middleName || ''} ${this.certificate.preferredLastName}`;
  }

  public ngOnInit() {
    this.busy = this.enrolmentCertificateResource
      .getCertificate(this.route.snapshot.params.tokenId)
      .subscribe(
        (certificate: EnrolmentCertificate) => {
          this.certificate = certificate;
          this.expiryDate = moment(this.certificate.expiryDate).isAfter(moment.now())
            ? moment(this.certificate.expiryDate).format('MMMM Do, YYYY') : null;
        },
        (error: any) => {
          this.toastService.openErrorToast(`Certificate is no longer valid.`);
          this.logger.error('[ProvisionerAccess] CertificateComponent::getCertificate error has occurred: ', error);
        }
      );
  }
}
