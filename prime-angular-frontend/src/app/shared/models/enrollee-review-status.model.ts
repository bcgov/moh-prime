import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { IdentificationDocument } from '@shared/models/identification-document.model';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';

export interface EnrolleeReviewStatus {
  enrolmentStatuses: EnrolmentStatus[];
  selfDeclarationDocuments: SelfDeclarationDocument[];
  identificationDocuments: IdentificationDocument[];
}
