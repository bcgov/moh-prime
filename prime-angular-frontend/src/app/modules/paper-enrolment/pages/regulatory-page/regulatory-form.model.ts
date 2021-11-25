import { HttpEnrollee } from '@shared/models/enrolment.model';

export interface RegulatoryForm extends Pick<HttpEnrollee, 'certifications' | 'deviceProviderIdentifier'> { }
