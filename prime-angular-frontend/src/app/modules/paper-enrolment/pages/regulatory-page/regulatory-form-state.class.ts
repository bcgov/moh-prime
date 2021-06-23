import { RegulatoryFormState as BaseRegulatoryPageFormState } from '@enrolment/pages/regulatory/regulatory-form-state';

export class RegulatoryFormState extends BaseRegulatoryPageFormState {
  /**
   * @description
   * Removes a certification from the list in response to an
   * emitted event from college certifications. Does not allow
   * the list of certifications to empty.
   *
   * @param index to be removed
   */
  public removeCertification(index: number) {
    this.certifications.removeAt(index);
  }
}
