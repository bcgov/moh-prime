import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';

export interface TermsOfAccess {
  id: number;
  enrolleeId: number;
  globalClauseId: number;
  globalClause: Clause;
  userClauseId: number;
  userClause: UserClause;
  licenseClassClauses: Clause[];
  limitsAndConditionsClauses: Clause[];
  effectiveDate: string;
}

export interface Clause {
  id: number;
  clause: string;
  effectiveDate: string;
}

export interface UserClause extends Clause {
  enrolleeClassification: EnrolleeClassification;
}
