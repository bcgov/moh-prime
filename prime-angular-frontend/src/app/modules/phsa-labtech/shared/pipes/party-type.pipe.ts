import { Pipe, PipeTransform } from '@angular/core';
import { PartyTypeEnum } from '../enums/party-type.enum';

@Pipe({
  name: 'partyType'
})
export class PartyTypePipe implements PipeTransform {
  public transform(partyType: PartyTypeEnum): string {
    return this.getPartyTypeAsString(partyType);
  }

  private getPartyTypeAsString(partyType: PartyTypeEnum) {
    switch (partyType) {
      case PartyTypeEnum.ENROLLEE:
        return 'Enrollee';
      case PartyTypeEnum.SIGNING_AUTHORITY:
        return 'Signing Authority';
      case PartyTypeEnum.LABTECH:
        return 'Labtech';
      case PartyTypeEnum.IMMUNIZER:
        return 'COVID-19 Immunizer';
      default:
        // Allow the DefaultPipe to determine a proper default value
        return '';
    }
  }
}
