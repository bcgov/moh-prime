import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';

import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { BaseEnrolmentPage } from './BaseEnrolmentPage';

export interface IBaseEnrolmentProfilePage {
  form: FormGroup;
  onSubmit(): void;
  canDeactivate(): Observable<boolean> | boolean;
}

export abstract class BaseEnrolmentProfilePage extends BaseEnrolmentPage implements IBaseEnrolmentProfilePage {
  public form: FormGroup;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog
  ) {
    super(route, router);
  }

  public abstract onSubmit(): void;

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  protected abstract createFormInstance(): void;
  protected abstract initForm(): void;
  protected abstract patchForm(): void;
}
