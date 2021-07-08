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

  /**
   * @description
   * Find the intersection between two arrays.
   */
  public static intersection<T>(arrX: T[], arrY: T[]) {
    return arrX.filter(x => arrY.includes(x));
  }

  /**
   * @description
   * Find the difference between two arrays.
   */
  public static difference<T>(arrX: T[], arrY: T[]) {
    return arrX.filter(x => !arrY.includes(x));
  }

  /**
   * @description
   * Find the union between two arrays.
   */
  public static union<T>(arrX: T[], arrY: T[]) {
    return arrX.concat(arrY);
  }

  /**
   * @description
   * Find outer-section between two arrays.
   */
  public static symmetricDifference<T>(arrX: T[], arrY: T[]) {
    return arrX
      .filter(x => !arrY.includes(x))
      .concat(
        arrY.filter(y => !arrX.includes(y)));
  }
}
