import { MatDrawerMode } from '@angular/material/sidenav';

export interface DashboardNavSection {
  items: DashboardNavSectionItem[];
}

export interface DashboardNavSectionItem {
  name: string;
  route: string | (string | number)[];
  icon?: string;
  showItem?: boolean;
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
   * Whether to show the dashboard navigation item icon.
   */
  showNavItemIcons: boolean;
  /**
   * @description
   * Whether to show the dashboard navigation item label.
   */
  showNavItemLabels: boolean;

  constructor(
    mode: MatDrawerMode,
    opened: boolean,
    fixedInViewport: boolean,
    showNavItemIcons: boolean,
    showNavItemLabels: boolean
  ) {
    this.mode = mode;
    this.opened = opened;
    this.fixedInViewport = fixedInViewport;
    this.showNavItemIcons = showNavItemIcons;
    // Force labels to be shown when icons are turned off
    this.showNavItemLabels = (!showNavItemIcons) ? true : showNavItemLabels;
  }
}
