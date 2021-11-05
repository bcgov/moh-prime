// TODO move into lib module which shouldn't have shared dependency
import { Address, AddressType, addressTypes } from '@lib/models/address.model';

export class AddressUtils {
  /**
   * @description
   * Ensure all address types contained within an object literal are
   * at least an object literal or instance of Address, and not null.
   */
  public static normalizeAddresses(model: { [key: string]: any }) {
    addressTypes.forEach((addressType: AddressType) => {
      if (model.hasOwnProperty(addressType)) {
        model[addressType] = this.normalizeAddress(model[addressType]);
      }
    });
  }

  /**
   * @description
   * Ensure all address types contained within an object literal are
   * null if they are an empty instance (or object literal) of an
   * Address.
   */
  public static denormalizeAddresses(model: { [key: string]: any }) {
    addressTypes.forEach((addressType: AddressType) => {
      if (model.hasOwnProperty(addressType)) {
        model[addressType] = this.denormalizeAddress(model[addressType]);
      }
    });
  }

  /**
   * @description
   * Convert an empty address to an instance of an Address.
   *
   * NOTE: Useful for converting a `null` address to an instance
   * of an address from an HTTP response.
   */
  public static normalizeAddress(address: Address): Address {
    return (!address)
      ? new Address()
      : address;
  }

  /**
   * @description
   * Convert an empty Address instance to null.
   *
   * NOTE: Useful for converting an empty address to a `null` for
   * an a HTTP request payload.
   */
  public static denormalizeAddress(address: Address): Address | null {
    return (Address.isNotEmpty(address))
      ? address
      : null;
  }
}
