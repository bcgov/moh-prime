import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { EMPTY, Subscription } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';

import { Role } from '@auth/shared/enum/role.enum';

import { EmailTemplate } from '@adjudication/shared/models/email-template.model';
import { EmailTemplateResourceService } from '@adjudication/shared/services/email-template-resource.service';

@Component({
  selector: 'app-notification-email-view',
  templateUrl: './notification-email-view.component.html',
  styleUrls: ['./notification-email-view.component.scss']
})
export class NotificationEmailViewComponent implements OnInit {
  @Input() public busy: Subscription;
  public emailTemplate: EmailTemplate;
  public subjectEditable: boolean;
  public templateEditable: boolean;
  public form: FormGroup;

  public Role = Role;

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private emailTemplateResource: EmailTemplateResourceService,
    private route: ActivatedRoute,
  ) {
    this.templateEditable = false;
  }

  public get template(): FormControl {
    return this.form.get('template') as FormControl;
  }

  public get subject(): FormControl {
    return this.form.get('subject') as FormControl;
  }

  public saveTemplate(): void {
    if (this.template.valid) {
      const data: DialogOptions = {
        title: 'Save Email Template',
        message: `Are you sure you want to overwrite the email template?`,
        actionText: 'Save Template'
      };

      this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: { output: string }) =>
            (result)
              ? this.emailTemplateResource.updateEmailTemplate(this.route.snapshot.params.eid, this.template.value)
              : EMPTY
          ),
        )
        .subscribe((emailTemplate: EmailTemplate) => {
          this.emailTemplate = emailTemplate;
          this.templateEditable = false;
        });
    }
  }

  public saveSubject(): void {
    if (this.subject.valid) {
      const data: DialogOptions = {
        title: 'Save Email Subject',
        message: `Are you sure you want to overwrite the email subject?`,
        actionText: 'Save Subject'
      };

      this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: { output: string }) =>
            (result)
              ? this.emailTemplateResource.updateEmailSubject(this.route.snapshot.params.eid, this.subject.value)
              : EMPTY
          ),
        )
        .subscribe((emailTemplate: EmailTemplate) => {
          this.emailTemplate = emailTemplate;
          this.subjectEditable = false;
        });
    }
  }

  public toggleEditTemplate(value: boolean): void {
    this.templateEditable = value;
    this.template.patchValue(this.emailTemplate?.template);
  }

  public toggleEditSubject(value: boolean): void {
    this.subjectEditable = value;
    this.subject.patchValue(this.emailTemplate?.subject);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.getEmailTemplate();
  }

  private createFormInstance(): void {
    this.form = this.fb.group({
      template: [this.emailTemplate?.template, [Validators.required]],
      subject: [this.emailTemplate?.subject, [Validators.required]],
    });
  }

  private getEmailTemplate() {
    this.busy = this.emailTemplateResource.getEmailTemplate(this.route.snapshot.params.eid)
      .subscribe((emailTemplate: EmailTemplate) => this.emailTemplate = emailTemplate);
  }
}
