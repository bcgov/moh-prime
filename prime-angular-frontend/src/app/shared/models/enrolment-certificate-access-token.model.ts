export interface EnrolmentCertificateAccessToken {
  id: string;
  userId: string;
  expires: string;
  viewCount: number;
  active: boolean;
  frontendUrl: string;
}
