import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { PartyTypeEnum } from '@phsa/shared/enums/party-type.enum';

export interface PhsaEnrollee extends Omit<BcscUser, 'username'> {
  id?: number;

  phone: string;
  phoneExtension?: string;

  partyTypes: PartyTypeEnum[];
}
