import { Pipe, PipeTransform } from '@angular/core';

import { StringUtils } from '@lib/utils/string-utils.class';
import { Address, optionalAddressLineItems } from '@lib/models/address.model';

@Pipe({
  name: 'address'
})
export class AddressPipe implements PipeTransform {
  public transform(address: Address, blacklist: (keyof Address)[] = optionalAddressLineItems): string {
    if (Address.isEmpty(address, blacklist)) {
      return '';
    }

    const street2 = (address?.street2) ? ` ${address?.street2}` : '';
    const postal = StringUtils.splice(address.postal.replace(/\s/g, '').toUpperCase(), 3, ' ');

    return `${address.street}${street2}, ${address.city} ${address.provinceCode}. ${postal}`;
  }
}
