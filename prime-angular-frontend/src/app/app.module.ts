import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { ConfigModule } from '@config/config.module';
import { CoreModule } from '@core/core.module';

import { AppRoutingModule } from './app-routing.module';
import { AppConfigModule } from './app-config.module';
import { AppComponent } from './app.component';

import { AuthModule } from '@auth/auth.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { ProvisionerAccessModule } from '@certificate/provisioner-access.module';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { RootRoutesModule } from '@lib/modules/root-routes/root-routes.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    ConfigModule,
    CoreModule,
    AppConfigModule,
    RootRoutesModule,
    AuthModule, // TODO lazy load this module
    EnrolmentModule, // TODO lazy load this module
    AdjudicationModule, // TODO lazy load this module
    ProvisionerAccessModule, // TODO lazy load this module
    SiteRegistrationModule, // TODO lazy load this module
    AppRoutingModule // WARNING: MUST be the last routing module imported!!!
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
