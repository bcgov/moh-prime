export class StringUtils {
  public static capitalize(value: string): string {
    return (value)
      ? `${value.charAt(0).toUpperCase()}${value.slice(1).toLowerCase()}`
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
