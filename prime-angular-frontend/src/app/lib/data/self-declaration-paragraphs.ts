import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';

export const selfDeclarationQuestions = {
  [SelfDeclarationTypeEnum.HAS_CONVICTION]: `Are you, or have you ever been, the subject of an order or a
    conviction under legislation in any jurisdiction for a matter that involved improper access to, collection,
    use, or disclosure of personal information?`,
  [SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED]: `Are you, or have you ever been, subject to any limits,
    conditions or prohibitions imposed as a result of disciplinary actions taken by a governing body of a health
    profession in any jurisdiction, that involved improper access to, collection, use, or disclosure of personal
    information?`,
  [SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION]: `Have you ever had your access to an electronic health record
    system, electronic medical record system, pharmacy or laboratory record system, or any similar health information
    system, in any jurisdiction, suspended or cancelled?`,
  [SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED]: `Have you ever been disciplined or fired by an employer, or had
    a contract for your services terminated, for a matter that involved improper access to, collection, use, or
    disclosure of personal information?`,
};
