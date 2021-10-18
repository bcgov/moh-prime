import { Component, OnInit, Input, TemplateRef, OnChanges, SimpleChanges } from '@angular/core';

export interface IProgressIndicator {
  inProgress: boolean;
  currentRoute: string;
  routes: string[];
}

/**
 * @description
 * Determine the percent complete based on a routes position
 * within a list of routes.
 */
@Component({
  selector: 'app-progress-indicator',
  templateUrl: './progress-indicator.component.html',
  styleUrls: ['./progress-indicator.component.scss']
})
export class ProgressIndicatorComponent implements OnInit, OnChanges, IProgressIndicator {
  /**
   * @description
   * Indicate whether progress has already been completed, and
   * calculation is not required since it's a 100% complete.
   */
  @Input() public inProgress: boolean;
  /**
   * @description
   * Current route for use in calculating progress, which is not
   * provided automatically to allow for greater reuse in
   * consuming components or modules.
   */
  @Input() public currentRoute: string;
  /**
   * @description
   * List of routes in the progressive order.
   */
  @Input() public routes: string[];
  /**
   * @description
   * Prefix for the default percent complete message.
   * e.g. "Registration" 25% Completed
   */
  @Input() public prefix: string;
  /**
   * @description
   * Customized message under the progressed indicator.
   * e.g. 100% complete and don't need percent complete,
   * but want to provide a message.
   */
  @Input() public message: string;
  /**
   * @description
   * <ng-template> for customizing the message, and
   * markup under the progress indicator.
   */
  @Input() public template: TemplateRef<any>;
  /**
   * @description
   * Acts as an override to display nothing under the progress
   * indicator, which becomes a glorified divider.
   */
  @Input() public noContent: boolean;
  /**
   * @description
   * Mode for displaying progress indicator information. Modes
   * are automatically determined based on input bindings, but
   * follow an order of precedence of none, template, message,
   * and percent (default).
   */
  public mode: 'none' | 'template' | 'message' | 'percent';
  /**
   * @description
   * Calculated percent complete.
   */
  public percentComplete: number;

  constructor() {
    this.routes = [];
  }

  /**
   * @description
   * Provide the template context locally scoped data.
   *
   * @example
   * Template with template reference (#indicator) for
   * passing to the progress indicator.
   *
   * <ng-template #indicator
   *              let-percentComplete="percentComplete"
   *              let-currentRoute="currentRoute"
   *              let-routes="routes">
   *   <app-example-component [currentRoute]="currentRoute"
   *                          [routes]="routes">
   *     You're {{ percentComplete }}% through registration!
   *   </app-example-component>
   * </ng-template>
   */
  public get templateContext() {
    return {
      inProgress: this.inProgress,
      routes: this.routes,
      currentRoute: this.currentRoute,
      percentComplete: this.percentComplete,
      prefix: this.prefix,
      message: this.message
    };
  }

  public ngOnChanges(changes: SimpleChanges): void {
    this.init();
  }

  public ngOnInit(): void {
    this.init();
  }

  private init() {
    // Setup order of precedence
    this.mode = (this.noContent) ? 'none'
      : (this.template) ? 'template'
        : (this.message) ? 'message'
          : 'percent'; // Default

    // Update the percent complete on any changes to
    // the input bindings
    this.updatePercentComplete();
  }

  private updatePercentComplete() {
    this.percentComplete = (this.inProgress)
      ? this.calculatePercentComplete()
      : 100;
  }

  private calculatePercentComplete() {
    const currentRouteIndex = this.routes.findIndex(r => r === this.currentRoute);
    const currentPage = (currentRouteIndex > -1) ? currentRouteIndex : 0;
    const totalPages = this.routes.length - 1;

    return Math.trunc(currentPage / totalPages * 100);
  }
}
