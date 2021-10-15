import { SatEnrollee } from '@sat/shared/models/sat-enrollee.model';

export interface RegulatoryForm extends Pick<SatEnrollee, 'partyCertifications'> {}
