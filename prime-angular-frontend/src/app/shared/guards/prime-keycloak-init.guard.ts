import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanLoad, Route, RouterStateSnapshot, UrlSegment, UrlTree } from '@angular/router';
import { KeycloakUtilsService } from '@core/services/keycloak-utils.service';
import { environment } from '@env/environment';
import { KeycloakOptions } from 'keycloak-angular';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PrimeKeycloakInitGuard implements CanActivate {

  constructor(
    private keycloakUtils: KeycloakUtilsService,
  ) { }
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    return this.keycloakUtils.initialize(environment.keycloakConfig as KeycloakOptions);
  }
}
