import { SiteNotification } from '@adjudication/shared/models/site-notification.model';
import { BaseAdjudicatorNote } from './adjudicator-note.model';

export interface SiteRegistrationNote extends BaseAdjudicatorNote {
  note: string;
  id: number;
  siteNotification: SiteNotification;
}
