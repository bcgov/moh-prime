import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';

import { ConfirmDiscardChangesDialogComponent } from '@shared/components/dialogs/confirm-discard-changes-dialog/confirm-discard-changes-dialog.component';
import { EnrolmentStateService } from '../../shared/services/enrolment-state.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit, OnDestroy {
  public form: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private enrolmentStateService: EnrolmentStateService
  ) { }

  public get voicePhone(): FormGroup {
    return this.form.get('voicePhone') as FormGroup;
  }

  public get voiceExtension(): FormGroup {
    return this.form.get('voiceExtension') as FormGroup;
  }

  public get hasContactEmail(): FormGroup {
    return this.form.get('hasContactEmail') as FormGroup;
  }

  public get contactEmail(): FormGroup {
    return this.form.get('contactEmail') as FormGroup;
  }

  public get hasContactPhone(): FormGroup {
    return this.form.get('hasContactPhone') as FormGroup;
  }

  public get contactPhone(): FormGroup {
    return this.form.get('contactPhone') as FormGroup;
  }

  public onSubmit() {
    if (this.form.valid) {
      this.form.markAsPristine();
      this.router.navigate(['professional'], { relativeTo: this.route.parent });
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

    this.hasContactEmail.valueChanges.subscribe((value: boolean) => {
      if (!value) {
        this.contactEmail.reset();
      }
    });
    this.hasContactPhone.valueChanges.subscribe((value: boolean) => {
      if (!value) {
        this.contactPhone.reset();
      }
    });
  }

  public ngOnDestroy() {

  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.contactForm;
  }
}
