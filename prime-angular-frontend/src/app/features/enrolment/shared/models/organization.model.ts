import { Moment } from 'moment';

export interface Organization {
  id?: number;
  name: string;
  organizationTypeCode: string;
  city: string;
  startDate: Moment;
  endDate: Moment;
}
