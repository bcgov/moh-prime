import { Injectable } from '@angular/core';
import { CanLoad, Route, UrlSegment, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, CanActivate } from '@angular/router';
import { KeycloakUtilsService } from '@core/services/keycloak-utils.service';
import { environment } from '@env/environment';
import { KeycloakOptions } from 'keycloak-angular';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MohKeycloakInitGuard implements CanActivate {

  constructor(
    protected keycloakUtils: KeycloakUtilsService,
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
    return this.keycloakUtils.initialize(environment.mohKeycloakConfig as KeycloakOptions);
  }
}
