import { Location } from './location.model';
import { Vendor } from './vendor.model';
import { Party } from './party.model';

export interface Site {
  id?: number;
  provisionerId: number;
  provisioner: Party;
  locationId: number;
  location: Location;
  vendorId: number;
  vendor: Vendor;
  pec: string;
  completed: boolean;
  approvedDate: string;
  submittedDate: string;
}
