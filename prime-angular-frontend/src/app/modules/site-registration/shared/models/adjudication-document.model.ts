import { Admin } from '@auth/shared/models/admin.model';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';

export interface AdjudicationDocument extends BaseDocument {
  adjudicatorId: number;
  uploadedDate: string;
  adjudicator: Admin;
}

export interface SiteAdjudicationDocument extends AdjudicationDocument {
  siteId: number;
}

export interface EnrolleeAdjudicationDocument extends BaseDocument {
  enrolleeId: number;
  adjudicatorId: number;
}
