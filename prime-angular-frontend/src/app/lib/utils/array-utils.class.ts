export class ArrayUtils {
  /**
   * @description
   * Conditional insert into an array when used in conjunction
   * with the spread operator.
   *
   * @example
   * const example = [1, 2, 3, ...ArrayUtils.insertIf(true, 4)] // [1, 2, 3, 4]
   * const example = [1, 2, 3, ...ArrayUtils.insertIf(false, 4)] // [1, 2, 3]
   */
  // TODO make response generic to provide stronger type safety
  public static insertIf(condition: any, ...elements: any[]): any[] {
    return (condition) ? [].concat(...elements) : [];
  }
}
