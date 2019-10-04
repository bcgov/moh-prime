export interface SelfDeclaration {
  has_conviction: boolean;
  conviction: SelfDeclarationIncident;
  has_suspension_cancellation: boolean;
  suspension_cancellation: SelfDeclarationIncident;
  has_disciplinary_action: boolean;
  disciplinary_action: SelfDeclarationIncident;
  has_pharmnet_suspension_revoke: boolean;
  suspension_revoke: SelfDeclarationIncident;
}

interface SelfDeclarationIncident {
  details: string;
  documents: any[];
}
