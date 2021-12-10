import { BusinessEventTypeEnum } from './business-event-type.model';

export interface BusinessEvent {
  enrolleeId: number;
  adminId: number;
  adminIDIR: string;
  businessEventTypeCode: BusinessEventTypeEnum;
  description: string;
  eventDate: string;
  partyName: string; // Signing Authority name
}
