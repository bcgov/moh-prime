import { Pipe, PipeTransform } from '@angular/core';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';

@Pipe({
  name: 'licenseNumberLabel'
})
export class LicenseNumberLabelPipe implements PipeTransform {
  public transform(collegeCode: number): string {
    switch (collegeCode) {
      case CollegeLicenceClassEnum.CPSBC:
        return 'CPSID Number';
      case CollegeLicenceClassEnum.CPBC:
      case CollegeLicenceClassEnum.CDSBC:
      case CollegeLicenceClassEnum.OptometryBC:
        return 'Registration Number';
      default:
        return 'Registration ID';
    };
  }
}
