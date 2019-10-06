export interface SelfDeclaration {
  hasConviction: boolean;
  // conviction: SelfDeclarationIncident;
  hasRegistrationSuspended: boolean;
  // suspension_cancellation: SelfDeclarationIncident;
  hasDisciplinaryAction: boolean;
  // disciplinary_action: SelfDeclarationIncident;
  hasPharmaNetSuspended: boolean;
  // suspension_revoke: SelfDeclarationIncident;
}

export interface SelfDeclarationIncident {
  details: string;
  documents: any[];
}
