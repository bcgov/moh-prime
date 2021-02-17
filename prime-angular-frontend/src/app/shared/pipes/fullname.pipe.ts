import { Pipe, PipeTransform } from '@angular/core';

// TODO add type param for given or preferred name
@Pipe({
  name: 'fullname'
})
export class FullnamePipe implements PipeTransform {
  public transform(model: { firstName: string, lastName: string, [key: string]: any }): string {
    if (!model) {
      return null;
    }

    const { firstName, lastName } = model;
    return (firstName && lastName)
      ? `${firstName} ${lastName}`
      : '';
  }

  // private getFullName(enrollee: Enrollee) {
  //   const firstName = (enrollee.firstName) ? enrollee.firstName : '';
  //   const lastName = (enrollee.lastName) ? enrollee.lastName : '';
  //   return ` ${firstName} ${lastName} `;
  // }

  // private getPreferredName(enrollee: Enrollee) {
  //   const preferredFirstName = (enrollee.preferredFirstName) ? enrollee.preferredFirstName : '';
  //   const preferredMiddleName = (enrollee.preferredMiddleName) ? enrollee.preferredMiddleName : '';
  //   const preferredLastName = (enrollee.preferredLastName) ? enrollee.preferredLastName : '';
  //   return ` ${preferredFirstName} ${preferredMiddleName} ${preferredLastName} `;
  // }
}
