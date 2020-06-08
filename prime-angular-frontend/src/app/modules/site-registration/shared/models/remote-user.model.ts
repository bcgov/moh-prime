import { RemoteUserLocation } from './remote-user-location.model';

export interface RemoteUser {
  id?: number;
  firstName: string;
  lastName: string;
  remoteUserLocations: RemoteUserLocation[];
}
