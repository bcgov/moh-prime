import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
// TODO businessDay array instead of form group
// import { BusinessDay } from '@registration/shared/models/business-day.model';

@Component({
  selector: 'app-business-hours-view',
  templateUrl: './business-hours-view.component.html',
  styleUrls: ['./business-hours-view.component.scss']
})
export class BusinessHoursViewComponent implements OnInit {
  @Input() public form: FormGroup;
  // TODO businessDay array instead of form group
  // @Input() public businessDays: BusinessDay[];
  @Output() public remove: EventEmitter<number>;

  constructor() {
    this.remove = new EventEmitter<number>();
  }

  public get businessDays(): FormArray {
    return this.form.get('businessDays') as FormArray;
  }

  public onRemove(index: number) {
    this.remove.emit(index);
  }

  public ngOnInit(): void { }
}
