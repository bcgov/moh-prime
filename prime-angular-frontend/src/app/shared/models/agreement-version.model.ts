import { AgreementType } from '@shared/enums/agreement-type.enum';

export interface AgreementVersion {
  id: number;
  effectiveDate: string;
  text: string;
  agreementType: AgreementType;
}
