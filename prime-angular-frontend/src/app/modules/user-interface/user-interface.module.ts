import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';
import { PageSimpleComponent } from '@ui/components/page-simple/page-simple.component';
import { AccessDeniedComponent } from '@ui/components/access-denied/access-denied.component';
import { UnsupportedComponent } from '@ui/components/unsupported/unsupported.component';
import { PageNotFoundComponent } from '@ui/components/page-not-found/page-not-found.component';
import { MaintenanceComponent } from '@ui/components/maintenance/maintenance.component';
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
    HelpComponent
  ]
})
export class UserInterfaceModule { }
