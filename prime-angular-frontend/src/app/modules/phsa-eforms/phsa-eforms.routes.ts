export class PhsaEformsRoutes {
  public static LOGIN_PAGE = 'phsa';

  public static MODULE_PATH = 'phsa-eforms';
  public static COLLECTION_NOTICE = 'collection-notice';

  public static ACCESS_CODE = 'access-code';
  public static DEMOGRAPHIC = 'demographic';
  public static AVAILABLE_ACCESS = 'available-access';
  public static SUBMISSION_CONFIRMATION = 'submission-confirmation';

  public static routePath(route: string): string {
    return `/${ PhsaEformsRoutes.MODULE_PATH }/${ route }`;
  }

  // Use by the progress indicator to calculate percent completion
  // of the enrolment process
  public static initialEnrolmentRouteOrder(): string[] {
    return [
      PhsaEformsRoutes.DEMOGRAPHIC,
      PhsaEformsRoutes.AVAILABLE_ACCESS,
      PhsaEformsRoutes.SUBMISSION_CONFIRMATION
    ];
  }
}
