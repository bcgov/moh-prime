import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';

export class SelfDeclarationDocument extends BaseDocument {
  enrolleeId: number;
  selfDeclarationTypeCode: number;
}
