import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';
import { AccessDeniedComponent } from '@ui/components/access-denied/access-denied.component';
import { PageNotFoundComponent } from '@ui/components/page-not-found/page-not-found.component';
import { MaintenanceComponent } from '@ui/components/maintenance/maintenance.component';
import { PageSimpleComponent } from './components/page-simple/page-simple.component';
import { AccessPreventedComponent } from './components/access-prevented/access-prevented.component';

@NgModule({
  imports: [
    SharedModule
  ],
  declarations: [
    AccessDeniedComponent,
    PageNotFoundComponent,
    MaintenanceComponent,
    PageSimpleComponent,
    AccessPreventedComponent
  ],
  exports: [
    PageNotFoundComponent,
    AccessDeniedComponent,
    MaintenanceComponent
  ]
})
export class UserInterfaceModule { }
