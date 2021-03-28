export class StringUtils {
  /**
   * @description
   * Capitalize the first letter of a string.
   */
  public static capitalize(value: string): string {
    return (value && typeof value === 'string')
      ? `${value.charAt(0).toUpperCase()}${value.slice(1).toLowerCase()}`
      : value;
  }

  /**
   * @description
   * Splice changes the contents of a string by removing or
   * replacing a string segment, and/or adding to the string.
   */
  public static splice(text: string, insertPosition: number, insertText: string, removeCount: number = 0): string {
    if (!text) {
      return text;
    }
    // When negative starts at the end of the string
    const calculatedPosition = (insertPosition < 0)
      ? text.length + insertPosition
      : insertPosition;
    return `${text.substring(0, calculatedPosition)}${insertText}${text.substring(calculatedPosition + removeCount)}`;
  }
}
