export class EnumUtils {
  /**
   * @description
   * Get the enum values for a numeric enum.
   *
   * NOTE: Will not work for non-numeric enums!
   */
  public static values<T>(enumDataType: T) {
    return Object.keys(enumDataType)
      .filter(k => typeof enumDataType[k as any] === 'number')
      .map(k => enumDataType[k as any]);
  }
}
