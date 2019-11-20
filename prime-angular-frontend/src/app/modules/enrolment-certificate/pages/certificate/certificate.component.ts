import { Component, OnInit } from '@angular/core';
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
    private enrolmentCertificateResource: EnrolmentCertificateResource
  ) { }

  ngOnInit() {
    this.busy = this.enrolmentCertificateResource.getCertificate('fa490742-6648-460c-84ae-b49e63fed257')
      .subscribe((certificate: EnrolmentCertificate) => {
        console.log(certificate);
      });
  }
}
