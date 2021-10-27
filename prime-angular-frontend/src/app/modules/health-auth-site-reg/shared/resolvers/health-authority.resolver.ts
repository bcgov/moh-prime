import { Injectable } from '@angular/core';
import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ObjectUtils } from '@lib/utils/object-utils.class';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

@Injectable({
  providedIn: 'root'
})
export class HealthAuthorityResolver implements Resolve<HealthAuthority> {
  constructor(
    private healthAuthorityResource: HealthAuthorityResource
  ) {}

  /**
   * @description
   * Considered a source of truth set directly from an
   * HTTP response, and made immutable.
   */
  public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<HealthAuthority> {
    const { haid } = route.params;
    return this.healthAuthorityResource.getHealthAuthorityById(haid)
      .pipe(
        map((healthAuthority: HealthAuthority) => ObjectUtils.deepFreeze(healthAuthority))
      );
  }
}
