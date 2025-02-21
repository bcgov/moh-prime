export interface SiteSubmission {
  id: number;
  siteId: number;
  profileSnapshot: HttpSite;
  createdDate: string;
}

/**
 * Create new interfaces for the site submission only,
 * do not reuse other classes or interfaces.
 * So that it will not have conflict changes to site view model etc.
 */
export interface HttpSite {
  // common site properties
  id: number;
  siteName: string;
  physicalAddress: HttpAddress;
  careSettingCode: number;
  pec: string;
  mnemonic: string;
  doingBusinessAs: string;
  submittedDate: string;
  status: number;
  deviceProviderId: string;

  businessHours: HttpBusinessHour[];

  // health authority properties
  healthAuthorityCareType: HttpHealthAuthorityCareType;
  healthAuthorityVendor: HttpHealthAuthorityVendor;
  healthAuthorityPharmanetAdministrator: HttpHealthAuthorityContact
  healthAuthorityTechnicalSupport: HttpHealthAuthorityContact;
  authorizedUser: HttpAuthorizedUser;

  // community site properties
  remoteUsers: HttpRemoteUsers[];
  siteVendors: HttpSiteVendor[];

  technicalSupport: HttpContact;
  privacyOfficer: HttpContact;
  administratorPharmaNet: HttpContact;

  businessLicence: HttpBusinessLicense;
}

export interface HttpDocument {
  filename: string;
  uploadedDate: string;
}

export interface HttpBusinessLicense {
  deferredLicenceReason: string;
  uploadedDate: string;
  expiryDate: string
  businessLicenceDocument: HttpDocument;
}

export interface HttpSiteVendor {
  vendor: HttpLookup;
}

export interface HttpLookup {
  name: string;
}

export interface HttpRemoteUsers {
  firstName: string;
  lastName: string;
  email: string;
  remoteUserCertification: HttpRemoteUserCertification;
}

export interface HttpRemoteUserCertification {
  licenseNumber: string;
  practitionerId: string;
  collegeCode: number;
  licenseCode: number;
}

export interface HttpBusinessHour {
  day: number;
  startTime: string;
  endTime: string;
}

export interface HttpHealthAuthorityCareType {
  careType: string;
}

export interface HttpHealthAuthorityVendor {
  vendor: HttpLookup;
}

export interface HttpHealthAuthorityContact {
  contact: HttpContact;
}

export interface HttpContact {
  firstName: string;
  lastName: string;
  jobRoleTitle: string;
  email: string;
  phone: string;
  fax: string;
  smsPhone: string;
  physicalAddress: HttpAddress;
}

export interface HttpAuthorizedUser {
  party: HttpParty;
}

export interface HttpParty {
  firstName: string;
  givenName: string;
  lastName: string;
  preferredFirstName: string;
  preferredMiddleName: string;
  preferredlastName: string;
  dateOfBirth: string;
  jobRoleTitle: string;
  email: string;
  phone: string;
  fax: string;
  smsPhone: string
}

export interface HttpAddress {
  provinceCode: string;
  street: string;
  street2: string;
  city: string;
  postal: string;
  countryCode: string;
}
