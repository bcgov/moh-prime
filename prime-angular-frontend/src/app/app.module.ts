import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { ConfigModule } from '@config/config.module';
import { CoreModule } from '@core/core.module';

import { AppRoutingModule } from './app-routing.module';
import { AppConfigModule } from './app-config.module';
import { AppComponent } from './app.component';

import { RootRoutesModule } from '@lib/modules/root-routes/root-routes.module';

@NgModule({
  declarations: [AppComponent],
  imports: [
    BrowserModule,
    ConfigModule,
    CoreModule,
    AppConfigModule,
    RootRoutesModule,
    AppRoutingModule // WARNING: MUST be the last routing module imported!!!
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}
