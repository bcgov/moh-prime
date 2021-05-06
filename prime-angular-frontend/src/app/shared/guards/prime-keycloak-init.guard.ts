import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, RouterStateSnapshot, UrlSegment, UrlTree } from '@angular/router';
import { AuthService } from '@auth/shared/services/auth.service';
import { KeycloakUtilsService } from '@core/services/keycloak-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { environment } from '@env/environment';
import { KeycloakOptions } from 'keycloak-angular';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PrimeKeycloakInitGuard implements CanActivate {

  constructor(
    private keycloakUtils: KeycloakUtilsService,
    protected authService: AuthService,
    protected logger: LoggerService
  ) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    try {
      this.authService.isLoggedIn();
      return true;
    } catch (error) {
      this.logger.error(error);
      return this.keycloakUtils.initialize(environment.keycloakConfig as KeycloakOptions);
    }
  }
}
