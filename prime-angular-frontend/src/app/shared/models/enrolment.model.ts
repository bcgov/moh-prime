import { Address } from '../../modules/enrolment/shared/models/address.model';
import { CollegeCertification } from '../../modules/enrolment/shared/models/college-certification.model';
import { Job } from '../../modules/enrolment/shared/models/job.model';
import { Organization } from '../../modules/enrolment/shared/models/organization.model';

export interface Enrolment {
  id?: number;
  enrollee: {
    id?: number;
    userId: string;
    firstName: string;
    middleName: string;
    lastName: string;
    preferredFirstName: string;
    preferredMiddleName: string;
    preferredLastName: string;
    dateOfBirth: string;
    physicalAddress: Address,
    mailingAddress?: Address,
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
  hasConvictionDetails: string;
  hasRegistrationSuspended: boolean;
  hasRegistrationSuspendedDetails: boolean;
  hasDisciplinaryAction: boolean;
  hasDisciplinaryActionDetails: boolean;
  hasPharmaNetSuspended: boolean;
  hasPharmaNetSuspendedDetails: boolean;
  organizations: Organization[];
}
