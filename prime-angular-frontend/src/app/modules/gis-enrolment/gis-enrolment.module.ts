import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { GisEnrolmentRoutingModule } from './gis-enrolment-routing.module';
import { GisDashboardComponent } from './shared/components/gis-dashboard/gis-dashboard.component';
import { CollectionNoticePageComponent } from './pages/collection-notice-page/collection-notice-page.component';

@NgModule({
  declarations: [
    CollectionNoticePageComponent,
  ],
  imports: [
    SharedModule,
    DashboardModule,
    GisEnrolmentRoutingModule
  ]
})
export class GisEnrolmentModule { }
