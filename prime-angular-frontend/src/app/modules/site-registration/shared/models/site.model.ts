import { Location } from './location.model';
import { Vendor } from './vendor.model';
import { Party } from './party.model';
import { RemoteUser } from './remote-user.model';
import { BusinessLicenceDocument } from './business-licence-document.model';

export interface Site {
  id?: number;
  provisionerId: number;
  provisioner: Party;
  locationId: number;
  location: Location;
  vendorCode: number;
  vendor: Vendor;
  remoteUsers: RemoteUser[];
  businessLicenceDocuments: BusinessLicenceDocument[];
  pec: string;
  completed: boolean;
  approvedDate: string;
  submittedDate: string;
}
