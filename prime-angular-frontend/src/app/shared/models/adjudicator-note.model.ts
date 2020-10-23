import { Admin } from '@auth/shared/models/admin.model';

export interface EnrolmentStatusAdjudicatorNote {
  adjudicatorNote: BaseAdjudicatorNote;
  id: number;
}

export interface BaseAdjudicatorNote {
  note: string;
  id: number;
  adudicatorId: number;
  adjudicator: Admin;
  noteDate?: string;
}
