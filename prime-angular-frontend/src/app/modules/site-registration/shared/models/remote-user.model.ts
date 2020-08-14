import { RemoteUserLocation } from './remote-user-location.model';
import { RemoteUserCertification } from './remote-user-certification.model';

export interface RemoteUser {
  id?: number;
  firstName: string;
  lastName: string;
  remoteUserLocations: RemoteUserLocation[];
  remoteUserCertifications: RemoteUserCertification[];
}
