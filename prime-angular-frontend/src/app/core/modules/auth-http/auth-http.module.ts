import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { JwtModule } from '@auth0/angular-jwt';

import { environment } from '../../../../environments/environment';

export function tokenGetter() {
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
