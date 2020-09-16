import { MatDrawerMode } from '@angular/material/sidenav';

/**
 * @description
 * Dashboard menu properties provided to the Angular
 * Material sidenav.
 */
export class DashboardMenuProps {
  /**
   * @description
   * Mode of the drawer; one of 'over', 'push' or 'side'.
   */
  public mode: MatDrawerMode;
  /**
   * @description
   * Whether the drawer is opened.
   */
  public opened: boolean;
  /**
   * @description
   * Whether the sidenav is fixed in the viewport.
   */
  public fixedInViewport: boolean;

  constructor(
    mode: MatDrawerMode,
    opened: boolean,
    fixedInViewport: boolean
  ) {
    this.mode = mode;
    this.opened = opened;
    this.fixedInViewport = fixedInViewport;
  }
}
