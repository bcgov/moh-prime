import { Address } from '@shared/models/address.model';

export interface DemographicForm {
  preferredFirstName?: string;
  preferredMiddleName?: string;
  preferredLastName?: string;
  physicalAddress?: Address;
  email: string;
  phone: string;
}
