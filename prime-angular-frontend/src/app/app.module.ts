import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { ConfigModule } from '@config/config.module';
import { CoreModule } from '@core/core.module';

import { AppRoutingModule } from './app-routing.module';
import { AppConfigModule } from './app-config.module';
import { AppComponent } from './app.component';

import { AuthModule } from '@auth/auth.module';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { ProvisionModule } from '@provision/provision.module';
import { AdminModule } from '@admin/admin.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    ConfigModule,
    CoreModule,
    AppConfigModule,
    AuthModule, // TODO: lazy load this module
    EnrolmentModule, // TODO: lazy load this module
    ProvisionModule, // TODO: lazy load this module
    AdminModule, // TODO: lazy load this module
    AppRoutingModule // WARNING: MUST be the last routing module imported!!!
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
