import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { AuthRoutingModule } from '@auth/auth-routing.module';
import { AuthComponent } from '@auth/shared/components/auth/auth.component';
import { InfoComponent } from './pages/info/info.component';

@NgModule({
  declarations: [
    AuthComponent,
    InfoComponent
  ],
  imports: [
    SharedModule,
    AuthRoutingModule
  ]
})
export class AuthModule { }
