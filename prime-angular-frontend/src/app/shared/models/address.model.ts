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
   */
  public static isEmpty(address: Address, blacklist: string[] = ['id']): boolean {
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
    return !this.isEmpty(address, blacklist);
  }
}
