import { NgModule, Optional, SkipSelf, ErrorHandler } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

import { throwIfAlreadyLoaded } from '@core/module-import-guard';

import { AuthHttpModule } from '@core/modules/auth-http/auth-http.module';
import { KeycloakModule } from '@core/modules/keycloak/keycloak.module';
import { ErrorHandlerInterceptor } from '@core/interceptors/error-handler.interceptor';
import { ErrorHandlerService } from '@core/services/error-handler.service';
import { PageNotFoundComponent } from '@core/components/page-not-found/page-not-found.component';
import { AccessDeniedComponent } from '@core/components/access-denied/access-denied.component';
import { MaintenanceComponent } from '@core/components/maintenance/maintenance.component';

@NgModule({
  imports: [
    BrowserModule,
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
    BrowserModule,
    BrowserAnimationsModule,
    AccessDeniedComponent,
    PageNotFoundComponent,
    MaintenanceComponent
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}
