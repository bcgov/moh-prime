import { Admin } from '@auth/shared/models/admin.model';
import { BaseAdjudicatorNote } from '@shared/models/adjudicator-note.model';

/**
 * @description
 * A single one-size fits all model for AdjudicatorNotes,
 * UserAgreementNote, and EnrolmentCertificateNote.
 */
export interface EnrolleeNote extends BaseAdjudicatorNote {
  enrolleeId: number;
  adudicatorId: number;
  adjudicator: Admin;
  note: string;
  noteDate?: string;
}
