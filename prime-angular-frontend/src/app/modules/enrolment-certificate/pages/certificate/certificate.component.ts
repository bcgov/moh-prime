import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs';

import { EnrolmentCertificate } from '../../shared/models/enrolment-certificate.model';
import { EnrolmentCertificateResource } from '../../shared/services/enrolment-certificate-resource.service';

@Component({
  selector: 'app-certificate',
  templateUrl: './certificate.component.html',
  styleUrls: ['./certificate.component.scss']
})
export class CertificateComponent implements OnInit {
  public busy: Subscription;
  public certificate: EnrolmentCertificate;

  constructor(
    private route: ActivatedRoute,
    private enrolmentCertificateResource: EnrolmentCertificateResource,
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

  public ngOnInit() {
    this.busy = this.enrolmentCertificateResource.getCertificate(this.route.snapshot.params.tokenId)
      .subscribe((certificate: EnrolmentCertificate) => this.certificate = certificate);
  }
}
