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
   * Checks whether each property of an address is empty.
   */
  public static isEmpty(address: Address): boolean {
    return !Object.keys(address)
      .filter(k => k !== 'id')
      .every(k => address[k] !== null);
  }
}
