export class Address {
  countryCode: string = null;
  provinceCode: string = null;
  street: string = null;
  street2: string = null;
  city: string = null;
  postal: string = null;

  constructor(
    countryCode: string = null,
    provinceCode: string = null,
    street: string = null,
    street2: string = null,
    city: string = null,
    postal: string = null
  ) {
    this.countryCode = countryCode;
    this.provinceCode = provinceCode;
    this.street = street;
    this.street2 = street2;
    this.city = city;
    this.postal = postal;
  }
}
