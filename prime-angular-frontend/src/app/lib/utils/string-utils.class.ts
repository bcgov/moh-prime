export class StringUtils {
  public static capitalize(value: string): string {
    return value.charAt(0).toUpperCase() + value.slice(1).toLowerCase();
  }

  public static splice(text: string, insertPosition: number, insertText: string, removeCount: number = 0): string {
    const calculatedPosition = (insertPosition < 0)
      ? text.length + insertPosition
      : insertPosition;
    return `${text.substring(0, calculatedPosition)}${insertText}${text.substring(calculatedPosition + removeCount)}`;
  }
}
