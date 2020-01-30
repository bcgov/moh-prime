import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { ConfigModule } from '@config/config.module';
import { CoreModule } from '@core/core.module';

import { AppRoutingModule } from './app-routing.module';
import { AppConfigModule } from './app-config.module';
import { AppComponent } from './app.component';

import { AuthModule } from '@auth/auth.module';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { EnrolmentCertificateModule } from './modules/provisioner-access/provisioner-access.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    ConfigModule,
    CoreModule,
    AppConfigModule,
    AuthModule, // TODO lazy load this module
    EnrolmentModule, // TODO lazy load this module
    AdjudicationModule, // TODO lazy load this module
    EnrolmentCertificateModule,
    AppRoutingModule // WARNING: MUST be the last routing module imported!!!
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
