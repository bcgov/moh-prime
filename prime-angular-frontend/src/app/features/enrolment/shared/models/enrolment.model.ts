import { Moment } from 'moment';
import { Address } from './address.model';
import { CollegeCertification } from './college-certification.model';
import { Job } from './job.model';
import { Organization } from './organization.model';

export interface Enrolment {
  enrollee: {
    userId: string;
    firstName: string;
    middleName: string;
    lastName: string;
    preferredFirstName: string;
    preferredMiddleName: string;
    preferredLastName: string;
    dateOfBirth: Moment;
    physicalAddress: Address,
    mailingAddress: Address,
    contactEmail: string;
    contactPhone: string;
    voicePhone: string;
    voiceExtension: string;
  };
  hasCertification: boolean;
  certifications: CollegeCertification[];
  isDeviceProvider: boolean;
  deviceProviderNumber: string;
  isInsulinPumpProvider: boolean;
  isAccessingPharmaNetOnBehalfOf: boolean;
  jobs: Job[];
  hasConviction: boolean;
  hasRegistrationSuspended: boolean;
  hasDisciplinaryAction: boolean;
  hasPharmaNetSuspended: boolean;
  organizations: Organization[];
}

export interface SelfDeclarationIncident {
  details: string;
  documents: any[];
}
