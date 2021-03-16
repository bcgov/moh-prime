import { Admin } from '@auth/shared/models/admin.model';

/**
 * @description
 *
 */
export interface EnrolleeNotification {
  id: number;
  enrolleeNoteId: number;
  adminId: number;
  admin: Admin;
  assigneeId: number;
  assignee: Admin;
}
