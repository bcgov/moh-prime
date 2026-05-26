export interface EmailsForCareSetting {
  emails: ProvisionerEmail[];
  careSettingCode: number;
  healthAuthorityCode?: number;
}

export interface ProvisionerEmail {
  email: string;
  remoteAccessSiteIds: number[];
}
