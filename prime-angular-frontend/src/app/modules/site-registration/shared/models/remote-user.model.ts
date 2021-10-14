import { RemoteUserCertification } from './remote-user-certification.model';

export interface RemoteUser {
  id?: number;
  firstName: string;
  lastName: string;
  email: string;
  remoteUserCertifications: RemoteUserCertification[];
  // `notified` is not displayed but we need to retain all fields to re-populate database
  notified: boolean;
}
