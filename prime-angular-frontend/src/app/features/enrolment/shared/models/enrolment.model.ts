import { Profile } from '../../pages/profile/profile.model';
import { Contact } from '../../pages/contact/contact.model';
import { ProfessionalInfo } from '../../pages/professional-info/professional-info.model';
import { SelfDeclaration } from '../../pages/self-declaration/self-declaratin.model';
import { PharmanetAccess } from '../../pages/pharmanet-access/pharmanet-access.model';

export interface Enrolment {
  profile: Profile;
  contact: Contact;
  professionalInfo: ProfessionalInfo;
  selfDeclaration: SelfDeclaration;
  pharmanetAccess: PharmanetAccess;
}
