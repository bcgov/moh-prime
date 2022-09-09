import { Injectable } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { ConfigService } from '@config/config.service';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';
import { NextStepsEnrolmentForm } from './next-steps-form.model';

@Injectable({
  providedIn: 'root'
})
export class NextStepsFormState extends AbstractFormState<NextStepsEnrolmentForm> {
  public constructor(
    protected fb: FormBuilder,
    protected configService: ConfigService
  ) {
    super();
    this.buildForm();
  }

  public get emails(): FormArray {
    return this.formInstance.get('emails') as FormArray;
  }

  public get json(): NextStepsEnrolmentForm {
    if (!this.formInstance) {
      return;
    }

    const { emails } = this.formInstance.getRawValue();

    return { emails }
  }

  public patchValue({ emails }: NextStepsEnrolmentForm): void {

    if (!this.formInstance || !Array.isArray(emails)) {
      return;
    }

    this.removeEmails();

    if (emails.length) {
      emails.forEach((e: string) => this.addEmail(e));
    }

    this.formInstance.patchValue({ emails });
  }

  public buildForm(): FormGroup {
    return this.formInstance = this.fb.group({
      emails: this.fb.array([]),
    });
  }

  public buildEmailForm(): FormGroup {
    return this.fb.group({
      email: ['', []]
    })
  }

  public addEmail(email?: string): void {
    const emailForm = this.buildEmailForm();
    emailForm.patchValue({ email });
    this.emails.push(emailForm);
  }

  public removeEmail(index: number): void {
    this.emails.removeAt(index);
  }

  public removeEmails() {
    this.emails.clear();
  }

  public addEmptyEmailInput(): void {
    this.addEmail();
  }
}
