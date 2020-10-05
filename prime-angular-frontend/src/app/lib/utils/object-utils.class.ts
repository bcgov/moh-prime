export class ObjectUtils {
  /**
   * @description
   * Map an object's keys by reference.
   */
  public static keyMapping(object: { [key: string]: any }, mapping: { [key: string]: string }) {
    Object.keys(object).forEach(oldKey => {
      const newKey = mapping[oldKey];
      const value = object[oldKey];
      object[newKey] = value;
      delete object[oldKey];
    });
  }
}
