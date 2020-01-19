import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';

export interface AccessTerm {
  id: number;
  enrolleeId: number;
  globalClauseId: number;
  globalClause: Clause;
  userClauseId: number;
  userClause: UserClause;
  licenseClassClauses: Clause[];
  limitsConditionsClause: Clause;
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
