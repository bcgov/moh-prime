import { Site } from '@registration/shared/models/site.model';

export interface BusinessLicenceForm extends Pick<Site, 'businessLicence' | 'doingBusinessAs' | 'pec' | 'activeBeforeRegistration'> { }
