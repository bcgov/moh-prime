import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';

/**
 * @deprecated
 * @see EnrolleeAgreement
 * @see OrganizationAgreement
 * @see Agreement (Base Class)
 */
// TODO drop usage of AccessTerm for Agreement
export interface AccessTerm {
  id: number;
  enrolleeId: number;
  agreementMarkup: string;
  effectiveDate: string;
  createdDate: string;
  acceptedDate?: string;
  expiryDate?: string;
}

export interface Clause {
  id: number;
  clause: string;
  effectiveDate: string;
}

export interface UserClause extends Clause {
  enrolleeClassification: EnrolleeClassification;
}
