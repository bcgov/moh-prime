export class StringUtils {
  public static capitalize(value: string): string {
    const strValue: String = new String(value);
    return (value)
      ? `${strValue.charAt(0).toUpperCase()}${strValue.slice(1).toLowerCase()}`
      : value;
  }

  public static splice(text: string, insertPosition: number, insertText: string, removeCount: number = 0): string {
    if (!text) {
      return text;
    }
    const calculatedPosition = (insertPosition < 0)
      ? text.length + insertPosition
      : insertPosition;
    return `${text.substring(0, calculatedPosition)}${insertText}${text.substring(calculatedPosition + removeCount)}`;
  }
}
