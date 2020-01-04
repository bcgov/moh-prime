export interface TermsOfAccess {
  id: number;
  enrolleeId: number;
  globalClauseId: number;
  globalClause: Clause;
  userClauseId: number;
  userClause: Clause;
  licenceClassClause: Clause[];
  limitsAndConditionsClause: Clause[];
}

export interface Clause {
  id: number;
  clause: string;
  effectiveDate: string;
}
