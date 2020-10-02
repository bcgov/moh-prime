export class EnrolmentRoutes {
  public static ENROLMENT = 'enrolment';
  public static COLLECTION_NOTICE = 'collection-notice';
  // Enrollee overview:
  public static OVERVIEW = 'overview';
  // Enrollee access:
  public static ACCESS_CODE = 'access-code';
  public static ID_SUBMISSION = 'id-submission';
  // Enrollee profile:
  public static BCEID_DEMOGRAPHIC = 'bceid-demographic';
  public static BCSC_DEMOGRAPHIC = 'bcsc-demographic';
  // Enrollee enrolment:
  public static REGULATORY = 'regulatory';
  public static REMOTE_ACCESS = 'remote-access';
  public static DEVICE_PROVIDER = 'device-provider';
  public static JOB = 'job';
  public static CARE_SETTING = 'care-setting';
  public static SELF_DECLARATION = 'self-declaration';
  // Enrolment submission:
  // Enrolment update was small, no auto or manual adjudication required, and
  // is NOT included in the submission or editable status route lists
  public static CHANGES_SAVED = 'changes-saved';
  public static SUBMISSION_CONFIRMATION = 'submission-confirmation';
  public static PENDING_ACCESS_TERM = 'pending-access-term';
  public static ACCESS_LOCKED = 'access-locked';
  public static ACCESS_DECLINED = 'access-declined';
  // Enrollee history and PharmaNet:
  // Replaces terms of access after accepting the terms of access (TOA)
  public static CURRENT_ACCESS_TERM = 'current-access-term';
  public static PHARMANET_ENROLMENT_SUMMARY = 'pharmanet-enrolment-summary';
  public static NOTIFICATION_CONFIRMATION = 'notification-confirmation';
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
      EnrolmentRoutes.PHARMANET_ENROLMENT_SUMMARY
    ];
  }

  // Enrollee profile routes are ordered from the perspective of an
  // "initial" enrolment. The order is important for directing the
  // enrollee incrementally through creating their profile
  public static enrolmentProfileRoutes(): string[] {
    return [
      // TODO shouldn't include identity access code or submission
      EnrolmentRoutes.ACCESS_CODE,
      EnrolmentRoutes.ID_SUBMISSION,
      EnrolmentRoutes.BCEID_DEMOGRAPHIC,
      EnrolmentRoutes.BCSC_DEMOGRAPHIC,
      EnrolmentRoutes.REGULATORY,
      EnrolmentRoutes.REMOTE_ACCESS,
      // EnrolmentRoutes.DEVICE_PROVIDER,
      EnrolmentRoutes.JOB,
      EnrolmentRoutes.CARE_SETTING,
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
      // ACCESS_TERM is synonymous with adjudicator manual/automatic APPROVED
      EnrolmentRoutes.PENDING_ACCESS_TERM
    ];
  }

  public static enrolmentEditableRoutes(): string[] {
    return [
      EnrolmentRoutes.CURRENT_ACCESS_TERM,
      EnrolmentRoutes.PHARMANET_ENROLMENT_SUMMARY,
      EnrolmentRoutes.NOTIFICATION_CONFIRMATION,
      EnrolmentRoutes.PHARMANET_TRANSACTIONS,
      EnrolmentRoutes.ACCESS_TERMS
    ];
  }

  // Accessible routes for an enrollee when they have been
  // approved for PharmaNet access and accepted a TOA
  public static enrolleeRoutes(): string[] {
    return [
      ...EnrolmentRoutes.enrolmentProfileRoutes(),
      ...EnrolmentRoutes.enrolmentEditableRoutes()
    ];
  }
}
