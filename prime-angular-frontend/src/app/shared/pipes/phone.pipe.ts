import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'phone'
})
export class PhonePipe implements PipeTransform {
  public transform(phoneNumber: string): any {
    if (!phoneNumber || /[a-zA-Z]/g.test(phoneNumber)) {
      return phoneNumber;
    }

    phoneNumber = phoneNumber.replace(/[\s]/g, '');
    const length = phoneNumber.length;

    if (length !== 10) {
      return phoneNumber;
    }

    const areaCode = phoneNumber.slice(length - 10, length - 7);
    const exchangeCode = phoneNumber.slice(length - 7, length - 4);
    const subscriberNumber = phoneNumber.slice(length - 4, length);

    return `(${areaCode}) ${exchangeCode}-${subscriberNumber}`;
  }
}
