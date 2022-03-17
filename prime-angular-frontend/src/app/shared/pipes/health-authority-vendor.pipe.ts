import { Pipe, PipeTransform } from '@angular/core';

import { VendorConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';

@Pipe({
  name: 'healthAuthorityVendor'
})
export class HealthAuthorityVendorPipe implements PipeTransform {
  constructor(
    private configService: ConfigService
  ) { }

  public transform(vendorCodes: number | number[]): string {
    vendorCodes = (Array.isArray(vendorCodes)) ? vendorCodes : [vendorCodes];
    return this.getReadableVendorList(vendorCodes);
  }

  private getReadableVendorList(vendorCodes: number[]): string {
    let vendorNames = vendorCodes.map((vendorCode: number) => this.getVendorName(vendorCode));
    return vendorNames.join(", ");
  }

  private getVendorName(vendorCode: number): string {
    let matches = this.configService.vendors
      .filter((vendorConfig: VendorConfig) => vendorConfig.code === vendorCode)
    return matches ? matches[0].name : '';
  }
}
