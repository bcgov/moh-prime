import { EnrolmentStatus } from './enrolment-status.model';
import { EnrolleeNote } from '@adjudication/shared/models/adjudication-note.model';
import { Adjudicator } from './adjudicator.model';

export interface EnrolmentStatusReference {
  id: number;
  enrolmentStatusId: number;
  enrolmentStatus: EnrolmentStatus;
  adjudicatorNoteId: number;
  adjudicatorNote: EnrolleeNote;
  adminId: number;
  adjudicator: Adjudicator;
}
