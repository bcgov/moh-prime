import { Admin } from '@auth/shared/models/admin.model';

/**
 * @description
 * A single one-size fits all model for AdjudicatorNotes,
 * UserAgreementNote, and EnrolmentCertificateNote.
 */
export interface AdjudicationNote {
  enrolleeId: number;
  adudicatorId: number;
  adjudicator: Admin;
  note: string;
  noteDate?: string;
}
