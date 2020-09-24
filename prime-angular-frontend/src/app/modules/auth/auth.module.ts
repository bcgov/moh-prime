import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';

import { AuthRoutingModule } from './auth-routing.module';
import { AuthComponent } from './shared/components/auth/auth.component';
import { PillComponent } from './shared/components/pill/pill.component';
import { InfoComponent } from './pages/info/info.component';
import { BceidComponent } from './pages/bceid/bceid.component';
import { AdminComponent } from './pages/admin/admin.component';
import { SiteComponent } from './pages/site/site.component';

@NgModule({
  declarations: [
    AdminComponent,
    AuthComponent,
    InfoComponent,
    BceidComponent,
    PillComponent,
    SiteComponent
  ],
  imports: [
    AuthRoutingModule,
    SharedModule,
    DashboardModule
  ]
})
export class AuthModule { }
