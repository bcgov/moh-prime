export class ObjectUtils {
  /**
   * @description
   * Map an object's keys by reference.
   */
  public static keyMapping(object: { [key: string]: any }, mapping: { [key: string]: string }) {
    if (!object || !mapping) {
      return;
    }

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
   * Map an object's keys by value.
   */
  public static keyMappingImmutable<T, S>(object: T, mapping: { [key: string]: string }): S {
    if (!object || !mapping) {
      return;
    }

    return Object.keys({ ...object })
      .reduce((mapped: S, key: string) => {
        mapped[mapping[key] ?? key] = object[key];
        return mapped;
      }, {} as S);
  }

  /**
   * @description
   * Merge a key/value pair into an object if the key
   * exists in the source object.
   */
  public static mergeInto(key: string, refObject: { [key: string]: any }, mergeObject: { [key: string]: any } = {}) {
    if (!key || !refObject || !mergeObject) {
      return mergeObject;
    }

    return (refObject[key])
      ? { ...mergeObject, [key]: refObject[key] }
      : mergeObject;
  }
}
