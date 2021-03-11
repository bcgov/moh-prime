import { PartyTypeEnum } from '../enums/party-type.enum';
import { PartyTypePipe } from './party-type.pipe';

describe('PartyTypePipe', () => {
  let pipe: PartyTypePipe;
  beforeEach(() => pipe = new PartyTypePipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should display the string representation of a party type', () => {
    const results = [
      [pipe.transform(PartyTypeEnum.ENROLLEE), 'Enrollee'],
      [pipe.transform(PartyTypeEnum.SIGNING_AUTHORITY), 'Signing Authority'],
      [pipe.transform(PartyTypeEnum.LABTECH), 'Labtech'],
      [pipe.transform(PartyTypeEnum.IMMUNIZER), 'COVID-19 Immunizer']
    ];
    results.forEach(result => expect(result[0]).toBe(result[1]));
  });

  it('should not fail when passed a unknown party type', () => {
    const result = pipe.transform(0);
    expect(result).toBe('');
  });
});
