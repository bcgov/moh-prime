import { Admin } from '@auth/shared/models/admin.model';

/**
 * @description
 *
 */
export interface SiteNotification {
  id: number;
  siteRegistrationNoteId: number;
  adminId: number;
  admin: Admin;
  assigneeId: number;
  assignee: Admin;
}
