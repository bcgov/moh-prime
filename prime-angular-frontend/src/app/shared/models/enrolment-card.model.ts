export interface EnrolmentCard {
  agreementId: number;
  submissionId: number;
  agreementType: string;
  agreementAcceptedDate: string;
  enrolmentApprovedDate: string;
  requestedRemoteAccess: boolean;
  submissionCreatedDate: string;
  isCurrent: boolean;
}
