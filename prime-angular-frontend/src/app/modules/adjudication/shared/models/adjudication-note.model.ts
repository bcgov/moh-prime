/**
 * @description
 * A single one-size fits all model for AdjudicatorNotes,
 * UserAgreementNote, and EnrolmentCertificateNote.
 */
export interface AdjudicationNote {
  enrolleeId: number;
  note: string;
  noteDate?: string;
}
