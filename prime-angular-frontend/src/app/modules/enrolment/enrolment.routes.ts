export class EnrolmentRoutes {
  public static ENROLMENT = 'enrolment';
  public static COLLECTION_NOTICE = 'collection-notice';
  public static PROFILE = 'profile';
  public static REGULATORY = 'regulatory';
  public static DEVICE_PROVIDER = 'device-provider';
  public static JOB = 'job';
  public static SELF_DECLARATION = 'self-declaration';
  public static ORGANIZATION = 'organization';
  public static REVIEW = 'review';
  public static CONFIRMATION = 'confirmation';
  public static ACCESS_AGREEMENT = 'access-agreement';
  public static SUMMARY = 'summary';
  public static DECLINED = 'declined';
  public static DECLINED_ACCESS_AGREEMENT = 'declined-access-agreement';

  public static MODULE_PATH = EnrolmentRoutes.ENROLMENT;

  public static routePath(route: string): string {
    return `/${EnrolmentRoutes.MODULE_PATH}/${route}`;
  }

  public static enrolmentRouteOrder(): string[] {
    return [
      EnrolmentRoutes.PROFILE,
      EnrolmentRoutes.REGULATORY,
      // EnrolmentRoutes.DEVICE_PROVIDER,
      EnrolmentRoutes.JOB,
      EnrolmentRoutes.SELF_DECLARATION,
      EnrolmentRoutes.ORGANIZATION,
      EnrolmentRoutes.REVIEW,
      ...this.postEnrolmentSubmissionRoutes()
    ];
  }

  public static postEnrolmentSubmissionRoutes(): string[] {
    return [
      EnrolmentRoutes.CONFIRMATION,
      EnrolmentRoutes.ACCESS_AGREEMENT,
      EnrolmentRoutes.SUMMARY,
      EnrolmentRoutes.DECLINED,
      EnrolmentRoutes.DECLINED_ACCESS_AGREEMENT
    ];
  }
}
