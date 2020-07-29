import { MatDrawerMode } from '@angular/material/sidenav';

export interface DashboardNavSection {
  items: DashboardNavSectionItem[];
}

export interface DashboardNavSectionItem {
  name: string;
  icon: string;
  route: string;
  showItem: boolean;
  disabled?: boolean;
  deemphasize?: boolean; // Reduce opacity
  forceActive?: boolean;
}

export class DashboardNavProps {
  /**
   * @description
   * Mode of the drawer; one of 'over', 'push' or 'side'.
   */
  mode: MatDrawerMode;
  /**
   * @description
   * Whether the drawer is opened.
   */
  opened: boolean;
  /**
   * @description
   * Whether the sidenav is fixed in the viewport.
   */
  fixedInViewport: boolean;
  /**
   * @description
   * Whether to show the icon, or the icon and label.
   */
  showNavItemLabels: boolean;

  constructor(mode: MatDrawerMode, opened: boolean, fixedInViewport: boolean, showLabel: boolean) {
    this.mode = mode;
    this.opened = opened;
    this.fixedInViewport = fixedInViewport;
    this.showNavItemLabels = showLabel;
  }
}
