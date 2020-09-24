import { Address } from '@shared/models/address.model';

import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { Vendor } from '@registration/shared/models/vendor.model';

export interface EnrolleeRemoteAccessSite {
  id: number;
  organizationName: string;
  physicalAddress: Address;
  remoteUsers: RemoteUser[];
  siteVendors: Vendor[];
}
