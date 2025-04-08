import { Component, OnInit } from '@angular/core';
import { UntypedFormGroup, UntypedFormBuilder, UntypedFormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs';

import { EnrolleeNote } from '@enrolment/shared/models/enrollee-note.model';
import { Role } from '@auth/shared/enum/role.enum';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-limits-conditions-clauses',
  templateUrl: './limits-conditions-clauses.component.html',
  styleUrls: ['./limits-conditions-clauses.component.scss'],
})
export class LimitsConditionsClausesComponent implements OnInit {
  public busy: Subscription;
  public form: UntypedFormGroup;
  public columns: string[];
  public preview: string;
  public hasActions: boolean;
  public editorConfig: Record<string, string>;
  public Role = Role;

  constructor(
    private route: ActivatedRoute,
    private fb: UntypedFormBuilder,
    private adjudicationResource: AdjudicationResource,
  ) {
    this.hasActions = false;
    this.editorConfig = {
      height: '25rem',
      base_url: '/tinymce',
      suffix: '.min',
      plugins: 'lists advlist',
      toolbar: 'undo redo | bold italic underline | bullist numlist outdent indent | removeformat',
      menubar: 'false'
    };
  }

  public get note(): UntypedFormControl {
    return this.form.get('note') as UntypedFormControl;
  }

  public onSubmit() {
    if (this.form.valid) {
      this.busy = this.adjudicationResource.updateAccessAgreementNote(this.route.snapshot.params.id, this.note.value)
        .subscribe();
    }
  }

  public onUpdate(event: { editor: any }) {
    if (!event.editor) { return; }
    this.preview = event.editor.getContent();
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
    this.busy = this.adjudicationResource.getAccessAgreementNote(this.route.snapshot.params.id)
      .subscribe((accessAgreementNote: EnrolleeNote) => {
        this.note.patchValue(accessAgreementNote?.note);
      });
  }

  private initForm() {
    this.note.valueChanges.subscribe((value: string) => this.preview = value);
  }

  private createFormInstance() {
    this.form = this.fb.group({
      note: [
        {
          value: '',
          disabled: false
        },
        []
      ]
    });
  }
}
