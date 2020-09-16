export type DashboardMenuType = 'route' | 'list';

export interface DashboardMenuDetail {
  readonly type: DashboardMenuType;
}

export interface DashboardMenuItemOptions {
  active?: boolean;
  deemphasize?: boolean;
  disabled?: boolean;
}

export abstract class DashboardMenuItem {
  public label: string;
  public icon: string;
  public options: DashboardMenuItemOptions;

  constructor(label: string, icon: string = null, options: DashboardMenuItemOptions = {}) {
    this.label = label;
    this.icon = icon;
    this.options = options;
  }

  /**
   * @description
   * Current state of the menu item options.
   */
  public getOptions(): DashboardMenuItemOptions {
    return {
      active: this.options?.active,
      disabled: this.options?.disabled,
      deemphasize: this.options?.deemphasize
    };
  }
}

export class DashboardRouteMenuItem extends DashboardMenuItem implements DashboardMenuDetail {
  public readonly type: 'route';
  /**
   * @description
   * Route path.
   */
  public route: (string | number)[] | string;
  /**
   * @description
   * Only active on exact route.
   */
  public exact?: boolean;

  constructor(label: string, route: string, icon: string = null, exact: boolean = false, options: DashboardMenuItemOptions = {}) {
    super(label, icon, options);

    this.route = route;
    this.exact = exact;
  }
}

export class DashboardListMenuItem extends DashboardMenuItem implements DashboardMenuDetail {
  public readonly type: 'list';
  public children: DashboardMenuItem[];

  constructor(label: string, children: DashboardMenuItem[], icon: string = null, options: DashboardMenuItemOptions = {}) {
    super(label, icon, options);

    this.children = children;
  }
}
