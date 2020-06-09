import { Location } from './location.model';
import { Vendor } from './vendor.model';
import { Party } from './party.model';
import { RemoteUser } from './remote-user.model';

export interface Site {
  id?: number;
  provisionerId: number;
  provisioner: Party;
  locationId: number;
  location: Location;
  vendorCode: number;
  vendor: Vendor;
  remoteUsers: RemoteUser[];
  businessLicences: [];
  pec: string;
  completed: boolean;
  approvedDate: string;
  submittedDate: string;
}
