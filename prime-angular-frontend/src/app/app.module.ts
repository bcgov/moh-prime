import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { ConfigModule } from '@config/config.module';
import { CoreModule } from '@core/core.module';

import { AppRoutingModule } from './app-routing.module';
import { AppConfigModule } from './app-config.module';
import { AppComponent } from './app.component';

// TODO: check bundle sizes based on lazy loading
// TODO: potentially lazy load both of these modules
// TODO: preload dashboard, etc; when in auth
import { AuthModule } from '@auth/auth.module';
import { DashboardModule } from '@dashboard/dashboard.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    ConfigModule,
    CoreModule,
    AppConfigModule,
    AuthModule, // TODO: lazy load this module
    DashboardModule, // TODO: lazy load this module
    AppRoutingModule // WARNING: MUST be the last routing module imported!!!
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
