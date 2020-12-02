import { Address } from '@shared/models/address.model';

export interface PhsaLabtech {
    firstName: string;
    lastName: string;
    givenNames: string;
    dateOfBirth: string;
    physicalAddress: Address;
    phone: string;
    phoneExtension?: string;
    email: string;
}
