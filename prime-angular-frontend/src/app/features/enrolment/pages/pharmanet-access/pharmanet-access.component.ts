import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';

import { ConfirmDiscardChangesDialogComponent } from '@shared/components/dialogs/confirm-discard-changes-dialog/confirm-discard-changes-dialog.component';

@Component({
  selector: 'app-pharmanet-access',
  templateUrl: './pharmanet-access.component.html',
  styleUrls: ['./pharmanet-access.component.scss']
})
export class PharmanetAccessComponent implements OnInit, OnDestroy {
  public form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog
  ) { }

  public onSubmit() {
    if (this.form.valid) {
      this.form.markAsPristine();
      this.router.navigate(['review'], { relativeTo: this.route.parent });
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
      organizations: this.fb.array([])
    });
  }
}
