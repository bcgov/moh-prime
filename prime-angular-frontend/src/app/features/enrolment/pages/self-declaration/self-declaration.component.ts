import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';

import { FormControlValidators } from '@shared/validators/form-control.validators';
import { ConfirmDiscardChangesDialogComponent } from '@shared/components/dialogs/confirm-discard-changes-dialog/confirm-discard-changes-dialog.component';

@Component({
  selector: 'app-self-declaration',
  templateUrl: './self-declaration.component.html',
  styleUrls: ['./self-declaration.component.scss']
})
export class SelfDeclarationComponent implements OnInit, OnDestroy {
  public form: FormGroup;
  public decisions: { code: boolean, name: string }[] = [
    { code: false, name: 'No' }, { code: true, name: 'Yes' }
  ];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog
  ) { }

  public get hasConviction(): FormGroup {
    return this.form.get('hasConviction') as FormGroup;
  }

  public get hasRegistrationSuspended(): FormGroup {
    return this.form.get('hasRegistrationSuspended') as FormGroup;
  }

  public get hasDisciplinaryAction(): FormGroup {
    return this.form.get('hasDisciplinaryAction') as FormGroup;
  }

  public get hasPharmaNetSuspended(): FormGroup {
    return this.form.get('hasPharmaNetSuspended') as FormGroup;
  }

  public onSubmit() {
    if (this.form.valid) {
      this.form.markAsPristine();
      this.router.navigate(['access'], { relativeTo: this.route.parent });
    } else {
      this.form.markAllAsTouched();
    }
  }

  public canDeactivate(): Observable<boolean> | boolean {
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDiscardChangesDialogComponent).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
  }

  public ngOnDestroy() {

  }

  private createFormInstance() {
    this.form = this.fb.group({
      hasConviction: [null, [FormControlValidators.requiredBoolean]],
      hasRegistrationSuspended: [null, [FormControlValidators.requiredBoolean]],
      hasDisciplinaryAction: [null, [FormControlValidators.requiredBoolean]],
      hasPharmaNetSuspended: [null, [FormControlValidators.requiredBoolean]],
    });
  }
}
