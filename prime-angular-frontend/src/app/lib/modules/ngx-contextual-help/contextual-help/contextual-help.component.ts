import { Component, Input, ContentChildren, QueryList } from '@angular/core';
import { MenuPositionX, MenuPositionY } from '@angular/material/menu';
import { ContextualTitleDirective } from '../contextual-title.directive';
import { ContextualContentDirective } from '../contextual-content.directive';
import { ThemePalette } from '@angular/material/core';

@Component({
  selector: 'app-contextual-help',
  templateUrl: './contextual-help.component.html',
  styleUrls: ['./contextual-help.component.scss']
})
export class ContextualHelpComponent {
  @Input() public xPosition: MenuPositionX;
  @Input() public yPosition: MenuPositionY;
  @Input() public icon: string;
  @Input() public color: ThemePalette;
  @Input() public small: boolean;
  @Input() public titleIcon: string;

  @ContentChildren(ContextualTitleDirective, { descendants: true })
  public contextualHelpTitleChildren: QueryList<ContextualTitleDirective>;
  @ContentChildren(ContextualContentDirective, { descendants: true })
  public contextualHelpContentChildren: QueryList<ContextualContentDirective>;

  constructor() {
    this.xPosition = 'after';
    this.yPosition = 'below';
    this.icon = 'info';
    this.color = 'primary';
    this.small = false;
  }
}
