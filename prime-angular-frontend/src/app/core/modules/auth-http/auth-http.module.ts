import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';

import { environment } from '@env/environment';

export function tokenGetter() {
  // TODO: store in local cookie intead in case of XSS?
  return localStorage.getItem('token');
}

@NgModule({
  imports: [
    HttpClientModule,
    JwtModule.forRoot({
      config: {
        tokenGetter,
        whitelistedDomains: environment.whiteListedDomain
      }
    })
  ]
})
export class AuthHttpModule { }
