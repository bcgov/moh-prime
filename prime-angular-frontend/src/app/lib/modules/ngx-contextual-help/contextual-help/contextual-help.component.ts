import { Component, Input, ContentChildren, QueryList } from '@angular/core';
import { MenuPositionX, MenuPositionY } from '@angular/material/menu';
import { ContextualTitleDirective } from '../contextual-title.directive';
import { ContextualContentDirective } from '../contextual-content.directive';

@Component({
  selector: 'app-contextual-help',
  templateUrl: './contextual-help.component.html',
  styleUrls: ['./contextual-help.component.scss']
})
export class ContextualHelpComponent {
  @Input() xPosition: MenuPositionX = 'after';
  @Input() yPosition: MenuPositionY = 'below';
  @Input() icon: string = 'info';
  @Input() color: string = 'primary';
  @Input() small: boolean = false;
  @Input() titleIcon: string;

  @ContentChildren(ContextualTitleDirective, { descendants: true })
  public contextualHelpTitleChildren: QueryList<ContextualTitleDirective>;
  @ContentChildren(ContextualContentDirective, { descendants: true })
  public contextualHelpContentChildren: QueryList<ContextualContentDirective>;

  constructor() { }
}
