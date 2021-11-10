import { FormGroup } from '@angular/forms';
import { RegulatoryFormState as BaseRegulatoryPageFormState } from '@enrolment/pages/regulatory/regulatory-form-state';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

export class RegulatoryFormState extends BaseRegulatoryPageFormState {
  public get selectedCollegeCodes(): number[] {
    return this.certifications.value
      .map((certification: CollegeCertification) => +certification.collegeCode);
  }

  public addEmptyCollegeCertification(): void {
    this.addCollegeCertification();
  }

  /**
   * @description
   * Removes a certification from the list in response to an
   * emitted event from college certifications. Does not allow
   * the list of certifications to empty.
   *
   * @param index to be removed
   */
  public removeCertification(index: number): void {
    this.certifications.removeAt(index);
  }

  /**
   * @description
   * Removes incomplete certifications from the list in preparation
   * for submission, and allows for an empty list of certifications.
   */
  public removeIncompleteCertifications(noEmptyCert: boolean = false): void {
    this.certifications.controls
      .forEach((control: FormGroup, index: number) => {
        // Remove if college code is "None" or the group is invalid
        if (!control.get('collegeCode').value || control.invalid) {
          this.removeCertification(index);
        }
      });

    // Always have a single cerfication available, and it prevents
    // the page from jumping too much when routing
    if (!noEmptyCert && !this.certifications.controls.length) {
      this.addEmptyCollegeCertification();
    }
  }
}
