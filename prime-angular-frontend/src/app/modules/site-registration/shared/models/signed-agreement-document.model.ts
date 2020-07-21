import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';

export interface SignedAgreementDocument extends BaseDocument {
  organizationId: number;
}
