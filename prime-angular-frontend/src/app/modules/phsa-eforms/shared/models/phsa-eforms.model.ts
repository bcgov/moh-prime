import { Address } from '@shared/models/address.model';

import { PartyTypeEnum } from '@phsa/shared/enums/party-type.enum';

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
