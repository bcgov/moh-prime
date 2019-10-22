import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { AuthRoutingModule } from './auth-routing.module';

import { AuthComponent } from './shared/components/auth/auth.component';
import { LoginFormComponent } from './shared/components/login-form/login-form.component';

import { LoginComponent } from './pages/login/login.component';
import { InfoComponent } from './pages/info/info.component';

@NgModule({
  declarations: [
    AuthComponent,
    LoginComponent,
    LoginFormComponent,
    InfoComponent
  ],
  imports: [
    SharedModule,
    AuthRoutingModule
  ]
})
export class AuthModule { }
