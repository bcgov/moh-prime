import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { AccessDeniedComponent } from './components/access-denied/access-denied.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { MaintenanceComponent } from './components/maintenance/maintenance.component';
import { PageSimpleComponent } from './components/page-simple/page-simple.component';
import { HelpComponent } from './components/help/help.component';
import { UnderagedComponent } from './components/underaged/underaged.component';

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
    UnderagedComponent
  ],
  exports: [
    PageSimpleComponent,
    AccessDeniedComponent,
    PageNotFoundComponent,
    MaintenanceComponent,
    HelpComponent,
    UnderagedComponent
  ]
})
export class RootRoutesModule { }
