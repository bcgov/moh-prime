import { AgreementType } from '@shared/enums/agreement-type.enum';

export interface Agreement {
  id: number;
  agreementContent: string; // HTML markup or Base64 (PDF)
  createdDate: string;
  acceptedDate: string;
  expiryDate: string;
}

export interface SignedAgreement {
  id: number;
}

export interface AgreementViewModel extends Agreement {
  agreementType: AgreementType;
  signedAgreementDocumentGuid: string;
}

export interface EnrolleeAgreement extends Agreement {
  enrolleeId: number;
  signedAgreement: SignedAgreement;
}

export interface EnrolleeAgreementViewModel extends EnrolleeAgreement, AgreementViewModel { }

export interface OrganizationAgreement extends Agreement {
  organizationId: number;
}

export interface OrganizationAgreementViewModel extends OrganizationAgreement, AgreementViewModel { }
