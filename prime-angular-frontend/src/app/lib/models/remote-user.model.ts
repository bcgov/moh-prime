import { RemoteUserCertification } from './remote-user-certification.model';

export interface RemoteUser {
  id?: number;
  firstName: string;
  lastName: string;
  email: string;
  remoteUserCertifications: RemoteUserCertification[];
  notified: boolean;
}
