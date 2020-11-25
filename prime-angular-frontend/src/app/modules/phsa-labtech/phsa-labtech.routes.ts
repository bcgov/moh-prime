export class PhsaLabtechRoutes {
  public static PHSA_LABTECH = 'phsa';

  public static EXAMPLE = 'example';
  public static ACCESS_CODE = 'access-code';

  public static MODULE_PATH = PhsaLabtechRoutes.PHSA_LABTECH;

  public static routePath(route: string): string {
    return `/${PhsaLabtechRoutes.MODULE_PATH}/${route}`;
  }
}
