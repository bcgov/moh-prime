import { PartyTypeEnum } from '@shared/enums/party-type.enum';
import { Address } from '@shared/models/address.model';

export interface PhsaEnrollee {
  firstName: string;
  lastName: string;
  givenNames: string;
  dateOfBirth: string;
  physicalAddress: Address;
  phone: string;
  phoneExtension?: string;
  email: string;
  partyTypes: PartyTypeEnum[];
}
