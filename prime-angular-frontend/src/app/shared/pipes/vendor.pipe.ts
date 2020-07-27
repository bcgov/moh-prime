import { Pipe, PipeTransform } from '@angular/core';

import { ConfigService } from '@config/config.service';

/**
 * @deprecated
 * Replaced by the ConfigCodePipe.
 * @see /config/config-code-pipe.ts
 */
@Pipe({
  name: 'vendor'
})
export class VendorPipe implements PipeTransform {
  constructor(
    private config: ConfigService
  ) { }

  public transform(vendorCode: number): string {
    const vendor = this.config.vendors.find(v => v.code === vendorCode);
    return (vendor)
      ? vendor.name
      : null;
  }
}
