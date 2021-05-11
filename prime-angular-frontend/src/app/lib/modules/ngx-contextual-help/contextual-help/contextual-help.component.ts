import { Component, Input, ContentChildren, QueryList, EventEmitter, Output } from '@angular/core';
import { MenuPositionX, MenuPositionY } from '@angular/material/menu';
import { ThemePalette } from '@angular/material/core';

import { ContextualTitleDirective } from '../contextual-title.directive';
import { ContextualContentDirective } from '../contextual-content.directive';

@Component({
  selector: 'app-contextual-help',
  templateUrl: './contextual-help.component.html',
  styleUrls: ['./contextual-help.component.scss']
})
export class ContextualHelpComponent {
  @Input() public xPosition: MenuPositionX;
  @Input() public yPosition: MenuPositionY;
  @Input() public icon: string;
  @Input() public menuIconColor: ThemePalette;
  @Input() public small: boolean;
  @Input() public outlined: boolean;
  @Input() public advanced: boolean;
  @Input() public titleIcon: string;
  @Output() public opened: EventEmitter<void>;

  @ContentChildren(ContextualTitleDirective, { descendants: true })
  public contextualHelpTitleChildren: QueryList<ContextualTitleDirective>;
  @ContentChildren(ContextualContentDirective, { descendants: true })
  public contextualHelpContentChildren: QueryList<ContextualContentDirective>;

  constructor() {
    this.xPosition = 'after';
    this.yPosition = 'below';
    this.icon = 'info';
    this.menuIconColor = 'primary';
    this.small = false;
    this.outlined = false;
    this.advanced = false;
    this.opened = new EventEmitter<void>();
  }

  public onOpen(event: Event) {
    event.stopPropagation();
    this.opened.emit();
  }
}
