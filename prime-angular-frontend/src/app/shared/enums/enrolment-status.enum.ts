export enum EnrolmentStatusEnum {
  EDITABLE = 1,
  UNDER_REVIEW,
  REQUIRES_TOA,
  LOCKED,
  DECLINED,
}

export const PaperEnrolmentStatusMap: Record<number, string> = {
  [EnrolmentStatusEnum.EDITABLE]: 'Complete',
  [EnrolmentStatusEnum.UNDER_REVIEW]: 'Incomplete'
};
