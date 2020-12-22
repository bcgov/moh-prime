export enum PartyTypeEnum {
  ENROLLEE = 1,
  SIGNING_AUTHORITY = 2,
  LABTECH = 3,
  IMMUNIZER = 4
}


export namespace PartyTypeEnum {
  export function text(partyType: PartyTypeEnum): string {
    switch (partyType) {
      case PartyTypeEnum.ENROLLEE:
        return 'Enrollee';
      case PartyTypeEnum.SIGNING_AUTHORITY:
        return 'Signing Authority';
      case PartyTypeEnum.LABTECH:
        return 'Labtech';
      case PartyTypeEnum.IMMUNIZER:
        return 'Covid - 19 Immunizer';
      default:
        return '-';
    }
  }
}
