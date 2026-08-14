import { Pipe, PipeTransform } from '@angular/core';
import { CollegeLicenceClassEnum } from '@shared/enums/college-licence-class.enum';
import { NaturopathicLicenceClassRequirePharmaNetID } from '@shared/enums/licence-class.enum';

@Pipe({
  name: 'licenseNumberLabel',
  standalone: false
})
export class LicenseNumberLabelPipe implements PipeTransform {
  public transform(collegeCode: number, LicenseCode: number): string {
    switch (collegeCode) {
      case CollegeLicenceClassEnum.CPSBC:
        return 'CPSID Number';
      case CollegeLicenceClassEnum.CPBC:
      case CollegeLicenceClassEnum.CDSBC:
      case CollegeLicenceClassEnum.OptometryBC:
        return 'Registration Number';
      default:
        return NaturopathicLicenceClassRequirePharmaNetID.some((id) => id === LicenseCode) ?
          'Licensee Number' : 'Registration ID';
    };
  }
}
