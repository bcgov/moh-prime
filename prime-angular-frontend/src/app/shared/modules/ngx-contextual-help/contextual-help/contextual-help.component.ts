import { Component, OnInit, Input, TemplateRef } from '@angular/core';
import { MenuPositionX, MenuPositionY } from '@angular/material';

@Component({
  selector: 'app-contextual-help',
  templateUrl: './contextual-help.component.html',
  styleUrls: ['./contextual-help.component.scss']
})
export class ContextualHelpComponent implements OnInit {
  @Input() xPosition: MenuPositionX = 'after';
  @Input() yPosition: MenuPositionY = 'below';
  @Input() template: TemplateRef<any>;

  constructor() { }

  public ngOnInit() { }
}
