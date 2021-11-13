import { Enrolment } from '@shared/models/enrolment.model';

export interface EnrolmentRegulatoryForm extends Pick<Enrolment, 'certifications' | 'deviceProviderIdentifier'> { }
