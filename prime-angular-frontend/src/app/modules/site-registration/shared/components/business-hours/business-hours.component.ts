import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';

import { BusinessDay } from '@registration/shared/models/business-day.model';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-business-hours',
  templateUrl: './business-hours.component.html',
  styleUrls: ['./business-hours.component.scss']
})
export class BusinessHoursComponent implements OnInit {
  @Input() public form: FormGroup;
  // TODO use json instead of passing in form to reduce dependencies
  // @Input() public businessDays: BusinessDay[];
  @Output() public add: EventEmitter<BusinessDay>;
  @Output() public remove: EventEmitter<number>;

  constructor() {
    this.add = new EventEmitter<BusinessDay>();
    this.remove = new EventEmitter<number>();
  }

  public get businessDays(): FormArray {
    return this.form.get('businessDays') as FormArray;
  }

  public onAdd(businessDay: BusinessDay) {
    this.add.emit(businessDay);
  }

  public onRemove(index: number) {
    this.remove.emit(index);
  }

  public ngOnInit(): void { }
}
