import { Location } from './location.model';
import { Vendor } from './vendor.model';
import { Party } from './party.model';

export interface Site {
  id?: number;
  locationId: number;
  location: Location;
  vendorId: number;
  vendor: Vendor;
  provisionerId: number;
  provisioner: Party;
  pec: string;
  completed: boolean;
  approvedDate: string;
}
