import { Address } from '@shared/models/address.model';
import { Person } from '@registration/shared/models/person.model';

export interface Contact extends Person {
  jobRoleTitle: string;
}
