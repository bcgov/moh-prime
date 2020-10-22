import { BaseAdjudicatorNote } from './adjudicator-note.model';

export interface SiteRegistrationNote extends BaseAdjudicatorNote {
  note: string;
  id: number;
}
