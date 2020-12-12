import { BusinessLicenceDocument } from './business-licence-document.model';

export class BusinessLicence {
  id: number;
  siteId: number;
  businessLicenceDocument: BusinessLicenceDocument;
  completed: boolean;
  deferredLicenceReason: string;

  constructor(siteId: number) {
    this.siteId = siteId;
  }
}
