import { BaseRoute } from '@core/models/base-route';

export class EnrolmentRoutes extends BaseRoute {
  public static get MODULE_PATH(): string {
    return EnrolmentRoutes.ENROLMENT;
  }

  // Module
  public static ENROLMENT = 'enrolment';
  // Enrolment
  public static PROFILE = 'profile';
  public static PROFESSIONAL_INFO = 'professional-info';
  public static SELF_DECLARATION = 'self-declaration';
  public static PHARMANET_ACCESS = 'pharmanet-access';
  public static REVIEW = 'review';
  // Post Enrolment
  public static CONFIRMATION = 'confirmation';
  public static ACCESS_AGREEMENT = 'access-agreement';
  public static SUMMARY = 'summary';

  public static postEnrolmentRoutes(): string[] {
    return [
      EnrolmentRoutes.CONFIRMATION,
      EnrolmentRoutes.ACCESS_AGREEMENT,
      EnrolmentRoutes.SUMMARY
    ];
  }
}
