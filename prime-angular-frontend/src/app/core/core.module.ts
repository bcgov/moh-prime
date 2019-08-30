import { NgModule, Optional, SkipSelf } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

import { throwIfAlreadyLoaded } from './module-import-guard';
import { AuthHttpModule } from './modules/auth-http/auth-http.module';

import { AccessDeniedComponent } from './components/access-denied/access-denied.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';

import { LoggerService } from './services/logger.service';
import { RouteStateService } from './services/route-state.service';
import { ToastService } from './services/toast.service';
import { UtilsService } from './services/utils.service';
import { ViewportService } from './services/viewport.service';
import { AuthTokenService } from './services/auth-token.service';
import { AuthService } from './services/auth.service';

import { AuthResource } from './resources/auth-resource.service';

@NgModule({
  imports: [
    BrowserAnimationsModule,
    HttpClientModule,
    AuthHttpModule
  ],
  providers: [
    LoggerService,
    RouteStateService,
    ToastService,
    UtilsService,
    ViewportService,
    AuthResource,
    AuthTokenService,
    AuthService
  ],
  declarations: [
    AccessDeniedComponent,
    PageNotFoundComponent
  ],
  exports: [
    PageNotFoundComponent,
    AccessDeniedComponent
  ]
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    throwIfAlreadyLoaded(parentModule, 'CoreModule');
  }
}
