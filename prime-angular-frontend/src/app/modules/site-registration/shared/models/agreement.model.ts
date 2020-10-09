export interface Agreement {
  id: number;
  organizationId: number;
  enrolleeId: number;
  partyId: number;
  signedAgreement: string;
  agreementMarkup: string;
  createdDate: string;
  acceptedDate: string;
  expiryDate: string;
}
