import { Injectable } from '@angular/core';

import { BaseGuard } from '@core/guards/base.guard';

@Injectable({
  providedIn: 'root'
})
export class HealthAuthSiteRegGuard extends BaseGuard { }
