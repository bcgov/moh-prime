export interface PharmanetAccess {
  organizations: Organizations[];
}

interface Organizations {
  name: string;
  type: string;
  city: string;
  start_date: string;
  end_date: string;
}
