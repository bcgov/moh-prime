import { Component, Input, Output, EventEmitter, QueryList, ContentChildren } from '@angular/core';
import { ContextualContentDirective } from '@shared/modules/ngx-contextual-help/contextual-content.directive';

@Component({
  selector: 'app-form-icon-group',
  templateUrl: './form-icon-group.component.html',
  styleUrls: ['./form-icon-group.component.scss']
})
export class FormIconGroupComponent {
  @Input() public show: boolean;
  @Input() public icon: string;
  // An empty tooltip message will not show a tooltip
  @Input() public tooltipMessage: string;
  @Output() public event: EventEmitter<void>;

  @ContentChildren(ContextualContentDirective, { descendants: false })
  public contextualHelpChildren: QueryList<ContextualContentDirective>;

  constructor() {
    this.show = true;
    this.icon = 'close';
    this.tooltipMessage = 'Remove';
    this.event = new EventEmitter<void>();
  }

  public onEvent() {
    this.event.emit();
  }
}
