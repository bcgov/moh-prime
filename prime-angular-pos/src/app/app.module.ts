import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppConfigModule } from './app-config.module';
import { AppComponent } from './app.component';

import { CoreModule } from '@core/core.module';
import { ConfigModule } from '@config/config.module';
import { AuthModule } from '@auth/auth.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    CoreModule,
    // TODO not needed for POS and would also remove the mocks
    ConfigModule,
    AppConfigModule,
    AppRoutingModule // WARNING: MUST be the last routing module imported!!!
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
