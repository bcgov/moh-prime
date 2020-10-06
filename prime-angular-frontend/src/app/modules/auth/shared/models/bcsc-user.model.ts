import { User } from './user.model';

export interface BcscUser extends User {
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
