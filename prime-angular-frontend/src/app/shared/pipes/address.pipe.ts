import { Pipe, PipeTransform } from '@angular/core';
import { Address, AddressLine } from '@shared/models/address.model';
import { StringUtils } from '@lib/utils/string-utils.class';

@Pipe({
  name: 'address'
})
export class AddressPipe implements PipeTransform {
  public transform(address: Address, blacklist: AddressLine[]): string {
    if (Address.isEmpty(address, blacklist)) {
      return '';
    }

    const street2 = (address?.street2) ? ` ${address?.street2}` : '';
    const postal = StringUtils.splice(address.postal.replace(/\s/g, '').toUpperCase(), 3, ' ');

    if (address?.street && address?.city && address?.provinceCode && address?.postal) {

    }

    return `${address.street}${street2}, ${address.city} ${address.provinceCode}. ${postal}`;
  }
}
