export class EnumUtils {
  /**
   * @description
   * Get the enum values for a numeric enum.
   *
   * NOTE: Will not work for non-numeric enums!
   */
  // TODO values<T extends enum>(enumeration: T), not possible yet!
  public static values<T>(enumeration: T): number[] {
    return Object.keys(enumeration)
      .filter(k => typeof enumeration[k as any] === 'number')
      .map(k => enumeration[k as any]);
  }

  /**
   * @description
   * Get the enum as a key/value paired object literal.
   *
   * @example
   * enum ExampleEnum {
   *   EXAMPLE_1 = 1,
   *   EXAMPLE_2,
   * }
   *
   * EnumUtils.asObject(ExampleEnum)
   * // Results:
   * // [
   * //   { key: 1, value: 'EXAMPLE_1' },
   * //   { key: 2, value: 'EXAMPLE_2' }
   * // ]
   *
   * NOTE: Will not work for non-numeric enums!
   */
  // TODO asObjects<T extends enum>(enumeration: T, ...), not possible yet!
  public static asObjects<T>(enumeration: T): { key: number, value: string }[] {
    return EnumUtils.values<T>(enumeration)
      .map(k => ({ key: k, value: enumeration[k] }));
  }
}
