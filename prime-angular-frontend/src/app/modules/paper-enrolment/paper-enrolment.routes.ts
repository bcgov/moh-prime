export class PaperEnrolmentRoutes {

  public static MODULE_PATH = 'paper-enrolment';

  public static DEMOGRAPHIC = 'demographic';

  public static routePath(route: string): string {
    return `/${PaperEnrolmentRoutes.MODULE_PATH}/${route}`;
  }

  public static routeOrder(): string[] {
    return [
      PaperEnrolmentRoutes.DEMOGRAPHIC
    ];
  }
}
