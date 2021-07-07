import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { EMPTY, Subscription } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';

import { Role } from '@auth/shared/enum/role.enum';

import { EmailTemplateTypeEnum } from '@adjudication/shared/models/email-template-type.model';
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
  public editable: boolean;
  public form: FormGroup;

  public EmailTemplateTypeEnum = EmailTemplateTypeEnum;
  public Role = Role;

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private emailTemplateResource: EmailTemplateResourceService,
    private route: ActivatedRoute,
  ) {
    this.editable = false;
  }

  public get template(): FormControl {
    return this.form.get('template') as FormControl;
  }

  public onSubmit(): void {
    if (this.form.valid) {
      const data: DialogOptions = {
        title: 'Save Email Template',
        message: `Are you sure you want to overrite the email template?`,
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
          this.editable = false;
        }
        );
    }
  }

  public toggleEdit(value: boolean): void {
    this.editable = value;
    this.template.patchValue(this.emailTemplate?.template);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.getEmailTemplate();
  }

  private createFormInstance(): void {
    this.form = this.fb.group({
      template: [this.emailTemplate?.template, [Validators.required]],
    });
  }

  private getEmailTemplate() {
    this.busy = this.emailTemplateResource.getEmailTemplate(this.route.snapshot.params.eid)
      .subscribe((emailTemplate: EmailTemplate) => this.emailTemplate = emailTemplate);
  }
}
