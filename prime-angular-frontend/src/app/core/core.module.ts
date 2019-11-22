import { NgModule, Optional, SkipSelf, ErrorHandler } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { throwIfAlreadyLoaded } from '@core/module-import-guard';

import { AccessDeniedComponent } from '@core/components/access-denied/access-denied.component';
import { PageNotFoundComponent } from '@core/components/page-not-found/page-not-found.component';
import { AuthHttpModule } from '@core/modules/auth-http/auth-http.module';
import { KeycloakModule } from '@core/modules/keycloak/keycloak.module';
import { ErrorHandlerInterceptor } from '@core/interceptors/error-handler.interceptor';
import { ErrorHandlerService } from '@core/services/error-handler.service';
import { MaintenanceComponent } from './components/maintenance/maintenance.component';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    AuthHttpModule,
    KeycloakModule
  ],
  providers: [
    {
      provide: ErrorHandler,
      useClass: ErrorHandlerService
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ErrorHandlerInterceptor,
      multi: true
    }
  ],
  declarations: [
    AccessDeniedComponent,
    PageNotFoundComponent,
    MaintenanceComponent
  ],
  exports: [
    PageNotFoundComponent,
    AccessDeniedComponent,
    MaintenanceComponent
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}
