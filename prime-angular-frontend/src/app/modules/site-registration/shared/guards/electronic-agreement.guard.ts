import { Injectable } from '@angular/core';
import { Params } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { BaseGuard } from '@core/guards/base.guard';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';

import { AuthService } from '@auth/shared/services/auth.service';

import { CareSettingEnum } from '@shared/enums/care-setting.enum';

@Injectable({
  providedIn: 'root'
})
export class ElectronicAgreementGuard extends BaseGuard {
  constructor(
    protected authService: AuthService,
    protected logger: ConsoleLoggerService,
    private organizationResource: OrganizationResource
  ) {
    super(authService, logger);
  }

  protected checkAccess(routePath: string = null, params: Params): Observable<boolean> {
    if (params?.csid && params?.oid) {
      return this.organizationResource.getCareSettingCodesForPendingTransfer(params.oid)
        .pipe(
          map((codes: CareSettingEnum[]) => {
            return codes.includes(+params.csid);
          })
        );
    }
    return of(false);
  }

}
