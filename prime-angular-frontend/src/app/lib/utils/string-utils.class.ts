export class StringUtils {
  public static capitalize(value: string): string {
    return value.charAt(0).toUpperCase() + value.slice(1).toLowerCase();
  }
}
