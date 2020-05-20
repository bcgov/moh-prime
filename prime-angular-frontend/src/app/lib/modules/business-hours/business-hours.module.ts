import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { BusinessHoursComponent } from './components/business-hours/business-hours.component';
import { BusinessHoursPickerComponent } from './components/business-hours-picker/business-hours-picker.component';
import { BusinessHoursViewComponent } from './components/business-hours-view/business-hours-view.component';
import { WeekdayPipe } from './pipes/weekday.pipe';

@NgModule({
  declarations: [
    BusinessHoursComponent,
    BusinessHoursPickerComponent,
    BusinessHoursViewComponent,
    WeekdayPipe
  ],
  imports: [
    SharedModule
  ],
  exports: [
    BusinessHoursComponent,
    WeekdayPipe
  ]
})
export class BusinessHoursModule { }
