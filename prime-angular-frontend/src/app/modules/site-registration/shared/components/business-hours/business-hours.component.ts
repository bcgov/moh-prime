import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { BusinessDay } from '@registration/shared/models/businessDay.model';

@Component({
  selector: 'app-business-hours',
  templateUrl: './business-hours.component.html',
  styleUrls: ['./business-hours.component.scss']
})
export class BusinessHoursComponent implements OnInit {
  @Input() public businessHours: BusinessDay[];
  @Output() public newBusinessHours: EventEmitter<BusinessDay[]>;
  constructor() { }

  ngOnInit(): void {
  }

}
