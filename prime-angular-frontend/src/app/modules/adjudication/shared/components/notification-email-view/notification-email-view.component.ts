import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
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
  public titleEditable: boolean;
  public descriptionEditable: boolean;
  public recipientEditable: boolean;
  public form: UntypedFormGroup;

  public Role = Role;

  constructor(
    private fb: UntypedFormBuilder,
    private dialog: MatDialog,
    private emailTemplateResource: EmailTemplateResourceService,
    private route: ActivatedRoute,
  ) {
    this.templateEditable = false;
  }

  public get template(): UntypedFormControl {
    return this.form.get('template') as UntypedFormControl;
  }

  public get subject(): UntypedFormControl {
    return this.form.get('subject') as UntypedFormControl;
  }

  public get emailTitle(): UntypedFormControl {
    return this.form.get("emailTitle") as UntypedFormControl;
  }

  public get description(): UntypedFormControl {
    return this.form.get("description") as UntypedFormControl;
  }

  public get recipient(): UntypedFormControl {
    return this.form.get("recipient") as UntypedFormControl;
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

  public saveTitle(): void {
    if (this.emailTitle.valid) {
      const data: DialogOptions = {
        title: 'Save Email Template Title',
        message: `Are you sure you want to overwrite the email template title?`,
        actionText: 'Save Title'
      };

      this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: { output: string }) =>
            (result)
              ? this.emailTemplateResource.updateEmailTitle(this.route.snapshot.params.eid, this.emailTitle.value)
              : EMPTY
          ),
        )
        .subscribe((emailTemplate: EmailTemplate) => {
          this.emailTemplate = emailTemplate;
          this.titleEditable = false;
        });
    }
  }

  public saveDescription(): void {
    if (this.description.valid) {
      const data: DialogOptions = {
        title: 'Save Email Description',
        message: `Are you sure you want to overwrite the email description?`,
        actionText: 'Save Description'
      };

      this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: { output: string }) =>
            (result)
              ? this.emailTemplateResource.updateEmailDescription(this.route.snapshot.params.eid, this.description.value)
              : EMPTY
          ),
        )
        .subscribe((emailTemplate: EmailTemplate) => {
          this.emailTemplate = emailTemplate;
          this.descriptionEditable = false;
        });
    }
  }

  public saveRecipient(): void {
    if (this.recipient.valid) {
      const data: DialogOptions = {
        title: 'Save Email Recipient',
        message: `Are you sure you want to overwrite the email recipient?`,
        actionText: 'Save Recipient'
      };

      this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: { output: string }) =>
            (result)
              ? this.emailTemplateResource.updateEmailRecipient(this.route.snapshot.params.eid, this.recipient.value)
              : EMPTY
          ),
        )
        .subscribe((emailTemplate: EmailTemplate) => {
          this.emailTemplate = emailTemplate;
          this.recipientEditable = false;
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

  public toggleEditTitle(value: boolean): void {
    this.titleEditable = value;
    this.emailTitle.patchValue(this.emailTemplate?.templateName);
  }

  public toggleEditDescription(value: boolean): void {
    this.descriptionEditable = value;
    this.description.patchValue(this.emailTemplate?.description);
  }

  public toggleEditRecipient(value: boolean): void {
    this.recipientEditable = value;
    this.recipient.patchValue(this.emailTemplate.recipient);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.getEmailTemplate();
  }

  private createFormInstance(): void {
    this.form = this.fb.group({
      template: [this.emailTemplate?.template, [Validators.required]],
      subject: [this.emailTemplate?.subject, [Validators.required]],
      emailTitle: [this.emailTemplate?.templateName, [Validators.required]],
      description: [this.emailTemplate?.description, [Validators.required]],
      recipient: [this.emailTemplate?.recipient, [Validators.required]],
    });
  }

  private getEmailTemplate() {
    this.busy = this.emailTemplateResource.getEmailTemplate(this.route.snapshot.params.eid)
      .subscribe((emailTemplate: EmailTemplate) => this.emailTemplate = emailTemplate);
  }
}
