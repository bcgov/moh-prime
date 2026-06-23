import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { AccessDeniedComponent } from './components/access-denied/access-denied.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { MaintenanceComponent } from './components/maintenance/maintenance.component';
import { PageSimpleComponent } from './components/page-simple/page-simple.component';
import { HelpComponent } from './components/help/help.component';
import { NotEligibleComponent } from './components/not-eligible/not-eligible.component';

@NgModule({
  imports: [
    SharedModule
  ],
  declarations: [
    PageSimpleComponent,
    AccessDeniedComponent,
    PageNotFoundComponent,
    MaintenanceComponent,
    HelpComponent,
    NotEligibleComponent,
  ],
  exports: [
    PageSimpleComponent,
    AccessDeniedComponent,
    PageNotFoundComponent,
    MaintenanceComponent,
    HelpComponent,
    NotEligibleComponent,
  ]
})
export class RootRoutesModule { }
