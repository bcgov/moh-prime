import { BusinessEventType } from './business-event-type.model';

export interface BusinessEvent {
  enrolleeId: number;
  adminId: number;
  adminIDIR: string;
  businessEventTypeCode: BusinessEventType;
  description: string;
  eventDate: string;
}
