import { AddressUtils } from '@lib/utils/address-utils.class';
import { Address } from '@lib/models/address.model';

describe('ArrayUtils', () => {
  const util = AddressUtils;

  describe('normalizeAddresses', () => {
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

    it('should create a new address when null is provided for an address type', () => {
      const result = {
        verifiedAddress: null,
        physicalAddress: null,
        mailingAddress: null
      };
      util.normalizeAddresses(result);
      expect(result).toEqual(jasmine.objectContaining({
        verifiedAddress: new Address(),
        physicalAddress: new Address(),
        mailingAddress: new Address()
      }));
    });

    it('should result in the same address that was provided for an address type', () => {
      const result = {
        verifiedAddress: address,
        physicalAddress: address,
        mailingAddress: address
      };
      util.normalizeAddresses(result);
      expect(result).toEqual(jasmine.objectContaining({
        verifiedAddress: address,
        physicalAddress: address,
        mailingAddress: address
      }));
    });
  });

  describe('denormalizeAddresses', () => {
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

    it('should result in null when an empty address is found for an address type', () => {
      const result = {
        verifiedAddress: new Address(),
        physicalAddress: new Address(),
        mailingAddress: new Address()
      };
      util.denormalizeAddresses(result);
      expect(result).toEqual(jasmine.objectContaining({
        verifiedAddress: null,
        physicalAddress: null,
        mailingAddress: null
      }));
    });

    it('should result in the same address that was provided for an address type', () => {
      const result = {
        verifiedAddress: address,
        physicalAddress: address,
        mailingAddress: address
      };
      util.denormalizeAddresses(result);
      expect(result).toEqual(jasmine.objectContaining({
        verifiedAddress: address,
        physicalAddress: address,
        mailingAddress: address
      }));
    });
  });

  describe('normalizeAddress', () => {
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

    it('should create a new address when null is provided', () => {
      const result = util.normalizeAddress(null);
      expect(result instanceof Address).toBeTruthy();
    });

    it('should result in the same address that was provided', () => {
      const result = util.normalizeAddress(address);
      expect(result).toEqual(jasmine.objectContaining(address));
    });
  });

  describe('denormalizeAddress', () => {
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

    it('should result in null when an empty address is provided', () => {
      const result = util.denormalizeAddress(new Address());
      expect(result).toBeNull();
    });

    it('should result in the same address that was provided', () => {
      const result = util.denormalizeAddress(address);
      expect(result).toEqual(jasmine.objectContaining(address));
    });
  });
});
