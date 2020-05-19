import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { BusinessDay } from '../../models/business-day.model';

@Component({
  selector: 'app-business-hours-view',
  templateUrl: './business-hours-view.component.html',
  styleUrls: ['./business-hours-view.component.scss']
})
export class BusinessHoursViewComponent implements OnInit {
  @Input() public businessDays: BusinessDay[];
  @Output() public remove: EventEmitter<number>;

  constructor() {
    this.remove = new EventEmitter<number>();
  }

  public onRemove(index: number) {
    this.remove.emit(index);
  }

  public ngOnInit(): void { }
}
