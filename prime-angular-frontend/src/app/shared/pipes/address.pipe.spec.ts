import { Address } from '@lib/models/address.model';

import { AddressPipe } from './address.pipe';

describe('AddressPipe', () => {
  let pipe: AddressPipe;
  beforeEach(() => pipe = new AddressPipe());

  it('create an instance', () => expect(pipe).toBeTruthy());

  it('should concatenate the minimum required address lines of an address', () => {
    const value = new Address('CA', 'BC', '65 Pine Crescent', null, 'Victoria', 'M4E 2B6');
    const result = pipe.transform(value);
    expect(result).toBe('65 Pine Crescent, Victoria BC. M4E 2B6');
  });

  it('should concatenate the all address lines of an address', () => {
    const value = new Address('CA', 'BC', '65 Pine Crescent', 'Road West', 'Victoria', 'M4E 2B6');
    const result = pipe.transform(value);
    expect(result).toBe('65 Pine Crescent Road West, Victoria BC. M4E 2B6');
  });

  it('should properly format the postal code of a Canadian address', () => {
    const value = new Address('CA', 'BC', '65 Pine Crescent', null, 'Victoria', 'm4e2b6');
    const result = pipe.transform(value);
    expect(result).toBe('65 Pine Crescent, Victoria BC. M4E 2B6');
  });

  it('should not fail when there is no address', () => {
    const value = null;
    const result = pipe.transform(value);
    expect(result).toBe('');
  });
});
