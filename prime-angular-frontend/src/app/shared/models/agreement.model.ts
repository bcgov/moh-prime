export interface Agreement {
  id: number;
  signedAgreement: string;
  agreementMarkup: string;
  createdDate: string;
  acceptedDate: string;
  expiryDate: string;
}

export interface EnrolleeAgreement extends Agreement {
  enrolleeId: number;
}

export interface OrganizationAgreement extends Agreement {
  organizationId: number;
}
