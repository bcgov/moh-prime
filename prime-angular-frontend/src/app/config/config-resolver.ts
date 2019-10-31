import { Injectable } from '@angular/core';
import { CanLoad, Route, UrlSegment, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Resolve } from '@angular/router';

import { Observable } from 'rxjs';

import { Configuration } from './config.model';
import { ConfigService } from './config.service';

@Injectable({
  providedIn: 'root'
})
export class ConfigResolver implements Resolve<Configuration> {
  constructor(
    private configService: ConfigService
  ) { }

  public resolve(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<Configuration> | Promise<Configuration> | Configuration {
    return this.configService.load();
  }
}
