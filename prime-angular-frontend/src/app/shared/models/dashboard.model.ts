export interface DashboardNavSection {
  header?: string;
  showHeader?: boolean;
  items: DashboardNavSectionItem[];
}

export interface DashboardNavSectionItem {
  name: string;
  icon: string;
  route: string;
  showItem: boolean;
  disabled?: boolean;
  // TODO refactor route children to limit or remove usage of forceActive
  forceActive?: boolean;
}
