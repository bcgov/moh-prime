import { AgreementType } from '@shared/enums/agreement-type.enum';

// TODO split out into view model
export interface Agreement {
  id: number;
  agreementType: AgreementType;
  // TODO rename to agreementContent to represent multiple formats
  agreementMarkup: string;
  signedAgreementDocumentGuid: string;
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
