export class EnrolmentRoutes {
  public static ENROLMENT = 'enrolment';
  // TODO remove collection notice, yes... no... yes... no...???
  public static COLLECTION_NOTICE = 'collection-notice';
  // Enrollee overview:
  public static OVERVIEW = 'overview';
  // Enrollee profile:
  public static DEMOGRAPHIC = 'demographic';
  public static REGULATORY = 'regulatory';
  public static DEVICE_PROVIDER = 'device-provider';
  public static JOB = 'job';
  public static SELF_DECLARATION = 'self-declaration';
  public static ORGANIZATION = 'organization';
  // Enrolment submission:
  public static SUBMISSION_CONFIRMATION = 'submission-confirmation';
  public static ACCESS_AGREEMENT = 'access-agreement';
  public static DECLINED = 'declined';
  public static DECLINED_ACCESS_AGREEMENT = 'declined-access-agreement';
  // Enrollee history and PharmaNet:
  // Replaces access agreement after accepting the terms of access (TOA)
  public static ACCESS_AGREEMENT_HISTORY = 'access-agreement-history';
  public static PHARMANET_ENROLMENT_CERTIFICATE = 'pharmanet-enrolment-certificate';
  public static PHARMANET_TRANSACTIONS = 'pharmanet-transactions';
  public static ENROLMENT_LOG_HISTORY = 'enrolment-log-history';

  public static MODULE_PATH = EnrolmentRoutes.ENROLMENT;

  public static routePath(route: string): string {
    return `/${EnrolmentRoutes.MODULE_PATH}/${route}`;
  }

  // Use by the progress indicator to calculate percent completion
  // of the enrolment process
  public static initialEnrolmentRouteOrder(): string[] {
    return [
      ...EnrolmentRoutes.enrolmentProfileRoutes(),
      ...EnrolmentRoutes.enrolmentSubmissionRoutes(),
      // Allows progress indicator to calculate 100%
      EnrolmentRoutes.PHARMANET_ENROLMENT_CERTIFICATE
    ];
  }

  // Enrollee profile routes are ordered from the perspective of an
  // "initial" enrolment.The order is important for directing the
  // enrollee incrementally through creating their profile
  public static enrolmentProfileRoutes(): string[] {
    return [
      EnrolmentRoutes.DEMOGRAPHIC,
      EnrolmentRoutes.REGULATORY,
      // EnrolmentRoutes.DEVICE_PROVIDER,
      EnrolmentRoutes.JOB,
      EnrolmentRoutes.SELF_DECLARATION,
      EnrolmentRoutes.ORGANIZATION,
      EnrolmentRoutes.OVERVIEW
    ];
  }

  // Enrolment submission routes are ordered from the perspective
  // of an initial or renewal enrolment that is submitted for manual
  // or automatic adjudication
  public static enrolmentSubmissionRoutes(): string[] {
    return [
      // Enrolment was flagged for manual adjudication
      EnrolmentRoutes.SUBMISSION_CONFIRMATION,
      EnrolmentRoutes.DECLINED,
      // ACCESS_AGREEMENT is synonymous with APPROVED
      EnrolmentRoutes.ACCESS_AGREEMENT,
      EnrolmentRoutes.DECLINED_ACCESS_AGREEMENT
    ];
  }

  public static enrolmentApprovedRoutes(): string[] {
    return [
      EnrolmentRoutes.ACCESS_AGREEMENT_HISTORY,
      EnrolmentRoutes.PHARMANET_ENROLMENT_CERTIFICATE,
      EnrolmentRoutes.PHARMANET_TRANSACTIONS,
      EnrolmentRoutes.ENROLMENT_LOG_HISTORY
    ];
  }

  // Accessible routes for an enrollee when they have been
  // approved for PharmaNet access, or are editing an
  // approved enrolment
  public static enrolleeRoutes(): string[] {
    return [
      ...EnrolmentRoutes.enrolmentProfileRoutes(),
      ...EnrolmentRoutes.enrolmentApprovedRoutes()
    ];
  }
}
