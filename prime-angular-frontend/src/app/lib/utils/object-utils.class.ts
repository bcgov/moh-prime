export class ObjectUtils {
  /**
   * @description
   * Map an object's keys by reference.
   */
  public static keyMapping(object: { [key: string]: any }, mapping: { [key: string]: string }) {
    Object.keys(object).forEach(oldKey => {
      const newKey = mapping[oldKey];
      object[newKey] = object[oldKey];
      delete object[oldKey];
    });
  }
}
