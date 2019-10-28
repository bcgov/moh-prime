export interface EnrolmentStatus {
  enrolmentId: number;
  statusCode: number;
  status: {
    code: number;
    name: string;
  };
  statusDate: string;
  isCurrent: boolean;
}
