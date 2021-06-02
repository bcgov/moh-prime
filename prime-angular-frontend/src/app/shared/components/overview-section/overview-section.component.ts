import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-overview-section',
  templateUrl: './overview-section.component.html',
  styleUrls: ['./overview-section.component.scss']
})
export class OverviewSectionComponent {
  /**
   * @description
   * Title of the overview section.
   */
  @Input() public title: string;
  /**
   * @description
   * Display the edit icon for redirection.
   */
  @Input() public showEditRedirect: boolean;
  /**
   * @description
   * Route path for editing a section.
   */
  @Input() public editRoute: string | (string | number)[];
  /**
   * @description
   * Route redirect tooltip.
   */
  @Input() public tooltip: string;
  /**
   * @description
   * Route event emitter.
   */
  @Output() public route: EventEmitter<string | (string | number)[]>;

  constructor() {
    this.route = new EventEmitter<string | (string | number)[]>();
  }

  public onRoute(routePath: string | (string | number)[]): void {
    this.route.emit(routePath);
  }
}
