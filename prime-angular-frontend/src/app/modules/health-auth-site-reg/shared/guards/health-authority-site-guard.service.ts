import { Injectable } from '@angular/core';

import { BaseGuard } from '@core/guards/base.guard';

@Injectable({
  providedIn: 'root'
})
export class HealthAuthoritySiteGuard extends BaseGuard {
  // TODO only needs to redirect to first page if doesn't exist, or
  //      overview if completed, otherwise allow passage
}
