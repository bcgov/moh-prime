import { Component, Input, ContentChildren, QueryList, TemplateRef } from '@angular/core';

export type AlertType = 'info' | 'warn' | 'danger';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss']
})
export class AlertComponent {
  /**
   * @description
   * Alert type indicating the theme.
   */
  @Input() public type: AlertType;
  /**
   * @description
   * Alert specific icon.
   */
  @Input() public icon: string;
  /**
   * @description
   * Additional CSS class(es).
   *
   * NOTE: Typically used to add a bottom margin, and
   * not structural changes.
   */
  @Input() public class: string;

  @ContentChildren('alertTitle', { descendants: true })
  public alertTitleChildren: QueryList<TemplateRef<any>>;
  @ContentChildren('alertContent', { descendants: true })
  public alertContentChildren: QueryList<TemplateRef<any>>;

  constructor() {
    this.type = 'info';
    this.icon = null;
    this.class = '';
  }

  public hasTitle(): boolean {
    return !!this.alertTitleChildren.length || false;
  }

  public hasIcon(): boolean {
    return this.icon !== null;
  }

  public getType(): string {
    return `alert-${this.type}`;
  }
}
