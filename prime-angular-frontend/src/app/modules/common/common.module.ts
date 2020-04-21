import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { PageSimpleComponent } from '@common/components/page-simple/page-simple.component';
import { AccessDeniedComponent } from '@common/components/access-denied/access-denied.component';
import { UnsupportedComponent } from '@common/components/unsupported/unsupported.component';
import { PageNotFoundComponent } from '@common/components/page-not-found/page-not-found.component';
import { MaintenanceComponent } from '@common/components/maintenance/maintenance.component';
import { HelpComponent } from '@common/components/help/help.component';
import { AccessDeclinedComponent } from './components/access-declined/access-declined.component';

@NgModule({
  imports: [
    SharedModule
  ],
  declarations: [
    AccessDeniedComponent,
    PageNotFoundComponent,
    MaintenanceComponent,
    PageSimpleComponent,
    UnsupportedComponent,
    HelpComponent,
    AccessDeclinedComponent
  ],
  exports: [
    PageNotFoundComponent,
    AccessDeniedComponent,
    MaintenanceComponent,
    HelpComponent,
    PageSimpleComponent,
    AccessDeclinedComponent
  ]
})
export class CommonModule { }
