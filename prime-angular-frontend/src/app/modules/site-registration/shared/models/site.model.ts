import { Location } from './location.model';
import { Vendor } from './vendor.model';
import { Party } from './party.model';
import { RemoteUser } from './remote-user.model';

export interface Site {
  id?: number;
  locationId: number;
  location: Location;
  vendorId: number;
  vendor: Vendor;
  provisionerId: number;
  provisioner: Party;
  remoteUsers: RemoteUser[];
  pec: string;
  completed: boolean;
  approvedDate: string;
  submittedDate: string;
}
