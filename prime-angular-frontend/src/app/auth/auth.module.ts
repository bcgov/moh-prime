import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { AuthRoutingModule } from '@auth/auth-routing.module';
import { AuthComponent } from '@auth/shared/components/auth/auth.component';
import { LoginComponent } from '@auth/pages/login/login.component';

@NgModule({
  declarations: [
    AuthComponent,
    LoginComponent
  ],
  imports: [
    SharedModule,
    AuthRoutingModule
  ]
})
export class AuthModule { }
