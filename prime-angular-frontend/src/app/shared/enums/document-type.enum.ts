const TOA = 'Terms of Access';
const NO_TYPE = 'Unspecified';
const SUPPORTING_ID = 'Supporting ID';
const PAPER_FORM = 'Paper Form';

export enum DocumentType {
  // Nullable on backend is converted to NoType
  // @see ConfigService
  NoType = 0,
  TOA = 1,
  SupportingID = 2,
  PaperForm = 3
}

export const DocumentSectionMap: Record<number, string> = {
  [DocumentType.TOA]: TOA,
  [DocumentType.NoType]: NO_TYPE,
  [DocumentType.SupportingID]: SUPPORTING_ID,
  [DocumentType.PaperForm]: PAPER_FORM
};
