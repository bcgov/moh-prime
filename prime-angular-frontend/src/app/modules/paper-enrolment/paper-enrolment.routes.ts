export class PaperEnrolmentRoutes {

  public static MODULE_PATH = 'paper-enrolment';

  public static DEMOGRAPHIC = 'demographic';
  public static CARE_SETTING = 'care-setting';
  public static REGULATORY = 'regulatory';
  public static OBO_SITES = 'obo-sites';
  public static SELF_DECLARATION = 'self-declaration';
  public static UPLOAD = 'upload';
  public static OVERVIEW = 'overview';
  public static NEXT_STEPS = 'next-steps';

  public static routePath(route: string): string {
    return `/${PaperEnrolmentRoutes.MODULE_PATH}/${route}`;
  }

  public static routeOrder(): string[] {
    return [
      PaperEnrolmentRoutes.DEMOGRAPHIC,
      PaperEnrolmentRoutes.CARE_SETTING,
      PaperEnrolmentRoutes.REGULATORY,
      PaperEnrolmentRoutes.OBO_SITES,
      PaperEnrolmentRoutes.SELF_DECLARATION,
      PaperEnrolmentRoutes.UPLOAD,
      PaperEnrolmentRoutes.NEXT_STEPS
    ];
  }
}
