import { Pipe, PipeTransform } from '@angular/core';

import { Enrolment } from '@shared/models/enrolment.model';
import { Enrollee } from '@shared/models/enrollee.model';

@Pipe({
  name: 'enrolment'
})
export class EnrolmentPipe implements PipeTransform {
  transform(enrolment: Enrolment, display: string): string {
    if (enrolment) {
      const enrollee = enrolment.enrollee;

      if (enrollee) {
        switch (display) {
          case 'preferredName':
            return this.getPreferredName(enrollee);
          case 'fullName':
            return this.getFullName(enrollee);
        }
      }
    }

    return null;
  }

  private getFullName(enrollee: Enrollee) {
    const firstName = (enrollee.firstName) ? enrollee.firstName : '';
    const middleName = (enrollee.middleName) ? enrollee.middleName : '';
    const lastName = (enrollee.lastName) ? enrollee.lastName : '';
    return ` ${firstName} ${middleName} ${lastName} `;
  }

  private getPreferredName(enrollee: Enrollee) {
    const preferredFirstName = (enrollee.preferredFirstName) ? enrollee.preferredFirstName : '';
    const preferredMiddleName = (enrollee.preferredMiddleName) ? enrollee.preferredMiddleName : '';
    const preferredLastName = (enrollee.preferredLastName) ? enrollee.preferredLastName : '';
    return ` ${preferredFirstName} ${preferredMiddleName} ${preferredLastName} `;
  }
}
