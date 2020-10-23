export class ObjectUtils {
  /**
   * @description
   * Map an object's keys by reference.
   */
  public static keyMapping(object: { [key: string]: any }, mapping: { [key: string]: string }) {
    Object.keys(object).forEach(oldKey => {
      const newKey = mapping[oldKey];
      if (newKey) {
        object[newKey] = object[oldKey];
        delete object[oldKey];
      }
    });
  }

  /**
   * @description
   * Merge a key/value pair into an object if it exists.
   */
  public static mergeInto(key: string, refObject: { [key: string]: any }, mergeObject: { [key: string]: any } = {}) {
    return (refObject[key])
      ? { ...mergeObject, [key]: refObject[key] }
      : mergeObject;
  }
}
