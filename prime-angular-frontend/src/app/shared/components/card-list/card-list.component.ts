import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

export interface CardListItem {
  icon: string;
  title: string;
  properties: {
    key: string;
    value: string;
  }[];
  action: {
    title: string;
    disabled: boolean;
  };
}

@Component({
  selector: 'app-card-list',
  templateUrl: './card-list.component.html',
  styleUrls: ['./card-list.component.scss']
})
export class CardListComponent implements OnInit {
  /**
   * @description
   * Indicate the type of item for use with actions.
   */
  @Input() public itemTypeLabel: string;
  /**
   * @description
   * List of card items and their associated properties.
   */
  @Input() public items: CardListItem[];
  /**
   * @description
   * Add an item event.
   */
  @Output() public add: EventEmitter<void>;
  /**
   * @description
   * Edit an item event.
   */
  @Output() public edit: EventEmitter<number>;
  /**
   * @description
   * Remove an item event.
   */
  @Output() public remove: EventEmitter<number>;

  constructor() {
    this.add = new EventEmitter<void>();
    this.edit = new EventEmitter<number>();
    this.remove = new EventEmitter<number>();
  }

  public onAdd(): void {
    this.add.emit();
  }

  public onEdit(index: number): void {
    this.edit.emit(index);
  }

  public onRemove(index: number): void {
    this.remove.emit(index);
  }

  public ngOnInit(): void { }
}
