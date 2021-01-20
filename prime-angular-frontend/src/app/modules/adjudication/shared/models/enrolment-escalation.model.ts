import { Admin } from '@auth/shared/models/admin.model';

/**
 * @description
 *
 */
export interface EnrolmentEscalation {
  id: number;
  enrolleeNoteId: number;
  adminId: number;
  admin: Admin;
  assigneeId: number;
  assignee: Admin;
}
