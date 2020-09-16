import { RemoteUserLocation } from './remote-user-location.model';
import { RemoteUserCertification } from './remote-user-certification.model';

export interface RemoteUser {
  id?: number;
  firstName: string;
  lastName: string;
  email: string;
  remoteUserLocations: RemoteUserLocation[];
  remoteUserCertifications: RemoteUserCertification[];
}
