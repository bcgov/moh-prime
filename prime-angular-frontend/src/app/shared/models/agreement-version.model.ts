import { AgreementType } from '@shared/enums/agreement-type.enum';

export interface AgreementVersion {
  id: number;
  updatedDate: string;
  agreementContent: string;
  agreementType: AgreementType;
}
