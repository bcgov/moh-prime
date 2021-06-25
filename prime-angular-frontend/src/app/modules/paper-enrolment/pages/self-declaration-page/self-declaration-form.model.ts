import { HttpEnrollee } from '@shared/models/enrolment.model';

export interface SelfDeclarationForm extends Pick<HttpEnrollee, 'selfDeclarations'> {}
