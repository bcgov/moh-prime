import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { AccessDeniedComponent } from './components/access-denied/access-denied.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { MaintenanceComponent } from './components/maintenance/maintenance.component';
import { PageSimpleComponent } from './components/page-simple/page-simple.component';
import { UnsupportedComponent } from './components/unsupported/unsupported.component';
import { HelpComponent } from './components/help/help.component';

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
    HelpComponent
  ],
  exports: [
    PageNotFoundComponent,
    AccessDeniedComponent,
    MaintenanceComponent,
    HelpComponent,
    PageSimpleComponent
  ]
})
export class RootRoutesModule { }
