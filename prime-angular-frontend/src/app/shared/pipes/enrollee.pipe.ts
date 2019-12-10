import { Pipe, PipeTransform } from '@angular/core';
import { Enrollee } from '@shared/models/enrollee.model';

@Pipe({
  name: 'enrollee'
})
export class EnrolleePipe implements PipeTransform {
  transform(enrollee: Enrollee, display: string): string {
    if (enrollee) {
      switch (display) {
        case 'preferredName':
          return this.getPreferredName(enrollee);
        case 'fullName':
          return this.getFullName(enrollee);
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
