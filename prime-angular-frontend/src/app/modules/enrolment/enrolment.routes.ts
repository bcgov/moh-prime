export class EnrolmentRoutes {
  public static ENROLMENT = 'enrolment';
  public static COLLECTION_NOTICE = 'collection-notice';
  // Enrollee overview:
  public static OVERVIEW = 'overview';
  // Enrollee profile:
  public static DEMOGRAPHIC = 'demographic';
  public static REGULATORY = 'regulatory';
  public static DEVICE_PROVIDER = 'device-provider';
  public static JOB = 'job';
  public static ORGANIZATION = 'organization';
  public static SELF_DECLARATION = 'self-declaration';
  // Enrolment submission:
  public static SUBMISSION_CONFIRMATION = 'submission-confirmation';
  public static TERMS_OF_ACCESS = 'terms-of-access';
  public static ACCESS_LOCKED = 'access-locked';
  // Enrollee history and PharmaNet:
  // Replaces terms of access after accepting the terms of access (TOA)
  public static CURRENT_ACCESS_TERM = 'current-access-term';
  public static PHARMANET_ENROLMENT_CERTIFICATE = 'pharmanet-enrolment-certificate';
  public static PHARMANET_TRANSACTIONS = 'pharmanet-transactions';
  public static ACCESS_TERMS = 'access-terms';

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
      EnrolmentRoutes.ORGANIZATION,
      EnrolmentRoutes.SELF_DECLARATION,
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
      EnrolmentRoutes.ACCESS_LOCKED,
      // TERMS_OF_ACCESS is synonymous with adjudicator manual/automatic APPROVED
      EnrolmentRoutes.TERMS_OF_ACCESS,
      EnrolmentRoutes.ACCESS_TERMS
    ];
  }

  public static enrolmentAcceptedToaRoutes(): string[] {
    return [
      EnrolmentRoutes.CURRENT_ACCESS_TERM,
      EnrolmentRoutes.PHARMANET_ENROLMENT_CERTIFICATE,
      EnrolmentRoutes.PHARMANET_TRANSACTIONS,
      EnrolmentRoutes.ACCESS_TERMS
    ];
  }

  // Accessible routes for an enrollee when they have been
  // approved for PharmaNet access and accepted a TOA
  public static enrolleeRoutes(): string[] {
    return [
      ...EnrolmentRoutes.enrolmentProfileRoutes(),
      ...EnrolmentRoutes.enrolmentAcceptedToaRoutes()
    ];
  }
}
