import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { Template } from '@angular/compiler/src/render3/r3_ast';

@Component({
  selector: 'app-summary-card',
  templateUrl: './summary-card.component.html',
  styleUrls: ['./summary-card.component.scss']
})
export class SummaryCardComponent implements OnInit {
  @Input() public icon: string;
  @Input() public title: string;
  @Input() public id: number;
  @Input() public actionButtonTitle: string;
  @Input() public actionDisabled: boolean;
  @Input() public properties: [string, string];
  @Input() public menu: Template;
  @Input() public outletContext: object;

  @Output() public action: EventEmitter<number>;
  @Output() public remove: EventEmitter<number>;

  constructor() {
    this.action = new EventEmitter<number>();
    this.remove = new EventEmitter<number>();
  }

  public onClick(id: number) {
    this.action.emit(id);
  }

  public onRemove(id: number) {
    this.remove.emit(id);
  }

  public ngOnInit(): void { }

}
