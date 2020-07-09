import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';

export interface SignedAgreement extends BaseDocument {
  organizationId: number;
}
