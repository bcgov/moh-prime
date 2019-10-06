export interface PharmanetAccess {
  organizations: Organizations[];
}

interface Organizations {
  id: number;
  name: string;
  organizationTypeCode: string;
  city: string;
  startDate: Date;
  endDate: string;
}
