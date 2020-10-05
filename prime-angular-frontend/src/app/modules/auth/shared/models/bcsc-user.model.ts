export interface BcscUser {
  userId: string;
  hpdid: string;
  firstName: string;
  lastName: string;
  givenNames: string;
  dateOfBirth: string;
  physicalAddress: {
    countryCode: string;
    provinceCode: string;
    street: string;
    city: string;
    postal: string;
  };
  email: string;
}
