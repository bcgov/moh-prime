import { Component, Input, ContentChildren, QueryList, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.scss']
})
export class AlertComponent {
  @Input() public type: 'info' | 'warn' | 'danger';
  @Input() public icon: string;
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
