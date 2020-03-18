import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { AuthRoutingModule } from './auth-routing.module';
import { AuthComponent } from './shared/components/auth/auth.component';
import { InfoComponent } from './pages/info/info.component';
import { AdminComponent } from './pages/admin/admin.component';
import { PillComponent } from './shared/components/pill/pill.component';
import { SiteComponent } from './pages/site/site.component';

@NgModule({
  declarations: [
    AdminComponent,
    AuthComponent,
    InfoComponent,
    PillComponent,
    SiteComponent
  ],
  imports: [
    SharedModule,
    AuthRoutingModule
  ]
})
export class AuthModule { }
