import { BaseRoute } from '@core/models/base-route';

export class EnrolmentRoutes extends BaseRoute {
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

  public static MODULE_PATH = EnrolmentRoutes.ENROLMENT;

  public static postEnrolmentRoutes(): string[] {
    return [
      EnrolmentRoutes.CONFIRMATION,
      EnrolmentRoutes.ACCESS_AGREEMENT,
      EnrolmentRoutes.SUMMARY
    ];
  }
}
