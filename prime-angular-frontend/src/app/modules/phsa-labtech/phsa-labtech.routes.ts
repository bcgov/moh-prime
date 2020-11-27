export class PhsaLabtechRoutes {
  public static PHSA_LABTECH = 'phsa';
  public static ACCESS_CODE = 'access-code';
  public static DEMOGRAPHIC = 'demographic';

  public static MODULE_PATH = PhsaLabtechRoutes.PHSA_LABTECH;

  public static routePath(route: string): string {
    return `/${PhsaLabtechRoutes.MODULE_PATH}/${route}`;
  }

  // Use by the progress indicator to calculate percent completion
  // of the enrolment process
  public static initialEnrolmentRouteOrder(): string[] {
    return [
      PhsaLabtechRoutes.ACCESS_CODE,
      PhsaLabtechRoutes.DEMOGRAPHIC
    ];
  }
}
