import { AddressLine } from '@lib/types/address-line.type';

export type AddressType = 'verifiedAddress' | 'physicalAddress' | 'mailingAddress';

export const addressTypes: AddressType[] = ['verifiedAddress', 'mailingAddress', 'physicalAddress'];

/**
 * @description
 * List of optional address line items.
 */
export const optionalAddressLineItems: ('id' | AddressLine)[] = ['id', 'street2'];

export class Address {
  id?: number = null;
  street: string = null;
  street2?: string = null;
  city: string = null;
  provinceCode: string = null;
  countryCode: string = null;
  postal: string = null;

  constructor(
    countryCode: string = null,
    provinceCode: string = null,
    street: string = null,
    street2: string = null,
    city: string = null,
    postal: string = null
  ) {
    this.street = street;
    this.street2 = street2;
    this.city = city;
    this.provinceCode = provinceCode;
    this.countryCode = countryCode;
    this.postal = postal;
  }

  /**
   * @description
   * Check for an empty address.
   *
   * NOTE: Most usecases don't require `street2`, and therefore it has
   * been excluded by default as optional.
   */
  public static isEmpty(address: Address, blacklist: string[] = optionalAddressLineItems): boolean {
    if (!address) {
      return false;
    }

    return Object.keys(address)
      .filter(key => !blacklist.includes(key))
      .every(k => !address[k]);
  }

  /**
   * @description
   * Checks for a partial address.
   */
  public static isNotEmpty(address: Address, blacklist?: string[]): boolean {
    if (!address) {
      return false;
    }

    return !this.isEmpty(address, blacklist);
  }
}
