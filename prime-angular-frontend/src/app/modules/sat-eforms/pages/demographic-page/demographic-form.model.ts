import { SatEnrollee } from '@sat/shared/models/sat-enrollee.model';

export interface DemographicForm extends Pick<SatEnrollee, 'partyCertifications'> {
  email: string;
  phone: string;
}
