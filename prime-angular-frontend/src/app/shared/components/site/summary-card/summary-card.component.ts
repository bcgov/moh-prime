import { Component, OnInit, Output, EventEmitter, Input, TemplateRef } from '@angular/core';
import { KeyValue } from '@angular/common';

@Component({
  selector: 'app-summary-card',
  templateUrl: './summary-card.component.html',
  styleUrls: ['./summary-card.component.scss']
})
export class SummaryCardComponent implements OnInit {
  @Input() public icon: string;
  @Input() public title: string;
  @Input() public menu: TemplateRef<any>;
  @Input() public menuOutletContext: { [key: string]: any } | null;
  @Input() public properties: KeyValue<string, string>[];
  @Input() public actionButtonTitle: string;
  @Input() public actionDisabled: boolean;

  @Output() public action: EventEmitter<void>;
  @Output() public remove: EventEmitter<void>;

  constructor() {
    this.action = new EventEmitter<void>();
    this.remove = new EventEmitter<void>();
  }

  public onAction() {
    this.action.emit();
  }

  public onRemove() {
    this.remove.emit();
  }

  public ngOnInit(): void { }
}
