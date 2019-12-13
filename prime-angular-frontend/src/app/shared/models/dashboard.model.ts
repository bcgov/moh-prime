export interface DashboardNavSection {
  header: string;
  showHeader: boolean;
  items: DashboardNavSectionItem[];
}

export interface DashboardNavSectionItem {
  name: string;
  icon: string;
  route: string;
  showItem: boolean;
  forceActive?: boolean;
}
