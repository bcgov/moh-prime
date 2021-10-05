import { BusinessLicenceDocument } from './business-licence-document.model';

export class BusinessLicence {
  id?: number;
  siteId: number;
  businessLicenceDocument: BusinessLicenceDocument;
  completed: boolean;
  deferredLicenceReason: string;
  expiryDate: string;

  constructor(
    siteId: number,
    expiryDate?: string
  ) {
    this.siteId = siteId;
    this.expiryDate = expiryDate;
  }
}
