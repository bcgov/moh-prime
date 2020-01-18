import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppConfigModule } from './app-config.module';
import { AppComponent } from './app.component';

import { CoreModule } from '@core/core.module';
import { ConfigModule } from '@config/config.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    CoreModule,
    ConfigModule,
    AppConfigModule,
    AppRoutingModule // WARNING: MUST be the last routing module imported!!!
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
