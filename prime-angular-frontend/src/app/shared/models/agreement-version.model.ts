import { AgreementType } from '@shared/enums/agreement-type.enum';

export interface AgreementVersion {
  id: number;
  updatedDate: string;
  text: string;
  agreementType: AgreementType;
}
