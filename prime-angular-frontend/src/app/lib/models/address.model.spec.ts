import { AddressUtils } from '@lib/utils/address-utils.class';
import { Address } from '@lib/models/address.model';

describe('ArrayUtils', () => {
  const util = AddressUtils;

  describe('instanceOf', () => {
    let address: Address;

    beforeEach(() => {
      address = {
        countryCode: 'CA',
        provinceCode: 'ON',
        street: '65 Pine Street',
        street2: null,
        city: 'Toronto',
        postal: 'M4E 2B6',
        id: 1
      };
    });

    it('should make the provided address object literal into an instance of an Address', () => {
      const result = Address.instanceOf(address);
      expect(address instanceof Address).toBeFalsy();
      expect(result instanceof Address).toBeTruthy();
    });

    it('should not change a provided address instance', () => {
      const instance = new Address('CA', 'ON', '65 Pine Street', null, 'Toronto', 'M4E 2B6');
      const result = Address.instanceOf(instance);
      expect(instance instanceof Address).toBeTruthy();
      expect(result instanceof Address).toBeTruthy();
      expect(result).toEqual(jasmine.objectContaining(instance));
    });
  });

  describe('isEmpty', () => {
    let address: Address;

    beforeEach(() => {
      address = {
        countryCode: 'CA',
        provinceCode: 'ON',
        street: '65 Pine Street',
        street2: null,
        city: 'Toronto',
        postal: 'M4E 2B6',
        id: 1
      };
    });

    it('should detect an empty address', () => {
      const result = Address.isEmpty(new Address());
      expect(result).toBeTruthy();
    });

    it('should detect a non empty address', () => {
      const result = Address.isEmpty(address);
      expect(result).toBeFalsy();
    });

    it('should be truthy when passed null', () => {
      const result = Address.isEmpty(null);
      expect(result).toBeTruthy();
    });
  });

  describe('isNotEmpty', () => {
    let address: Address;

    beforeEach(() => {
      address = {
        countryCode: 'CA',
        provinceCode: 'ON',
        street: '65 Pine Street',
        street2: null,
        city: 'Toronto',
        postal: 'M4E 2B6',
        id: 1
      };
    });

    it('should detect an empty address', () => {
      const result = Address.isNotEmpty(new Address());
      expect(result).toBeFalsy();
    });

    it('should detect a non empty address', () => {
      const result = Address.isNotEmpty(address);
      expect(result).toBeTruthy();
    });

    it('should be truthy when passed null', () => {
      const result = Address.isNotEmpty(null);
      expect(result).toBeFalsy();
    });
  });
});
