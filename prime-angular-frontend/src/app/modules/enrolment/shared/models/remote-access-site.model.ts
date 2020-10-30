import { Site } from '@registration/shared/models/site.model';

export interface RemoteAccessSite {
  id: number;
  enrolleeId: number;
  siteId: number;
  site: Site;
}
