import { EnrolmentStatus } from './enrolment-status.model';
import { AdjudicationNote } from '@adjudication/shared/models/adjudication-note.model';
import { Adjudicator } from './adjudicator.model';

export interface EnrolmentStatusReference {
  id: number;
  enrolmentStatusId: number;
  enrolmentStatus: EnrolmentStatus;
  adjudicatorNoteId: number;
  adjudicatorNote: AdjudicationNote;
  adminId: number;
  adjudicator: Adjudicator;
}
