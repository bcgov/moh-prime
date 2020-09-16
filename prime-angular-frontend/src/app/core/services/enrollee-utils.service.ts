import { Injectable } from '@angular/core';
import { Enrolment } from '@shared/models/enrolment.model';
import { ConfigService } from '@config/config.service';

@Injectable({
  providedIn: 'root'
})
export class EnrolleeUtilsService {

  constructor(
    private configService: ConfigService
  ) { }

  public isRegulatedUser(enrolment: Enrolment): boolean {
    return enrolment.certifications
      .map(cert => this.configService.licenses.find(l => l.code === cert.licenseCode))
      .some(l => l.regulatedUser);
  }
}
