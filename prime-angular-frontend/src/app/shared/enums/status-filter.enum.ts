import { EnrolmentStatusEnum } from './enrolment-status.enum';

export enum PaperStatusEnum {
  UNLINKED_PAPER_ENROLMENT = 42, // Codes are arbitrary and only used by the frontend
  LINKED_PAPER_ENROLMENT = 43
}

export enum EnrolmentStatusFilterEnum {
  RENEWED_ENROLMENT = 420
}

export type StatusFilterEnum = EnrolmentStatusEnum | PaperStatusEnum | EnrolmentStatusFilterEnum;
