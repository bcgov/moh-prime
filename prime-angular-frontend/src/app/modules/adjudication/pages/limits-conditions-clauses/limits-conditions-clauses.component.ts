import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { Subscription } from 'rxjs';

import { HttpEnrollee } from '@shared/models/enrolment.model';
import { Role } from '@auth/shared/enum/role.enum';

import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-limits-conditions-clauses',
  templateUrl: './limits-conditions-clauses.component.html',
  styleUrls: ['./limits-conditions-clauses.component.scss'],
})
export class LimitsConditionsClausesComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public columns: string[];
  public enrollee: HttpEnrollee;
  public preview: string;
  public hasActions: boolean;
  public editorConfig: Record<string, string>;
  public Role = Role;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
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

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
  }

  public onSubmit() {
    if (this.form.valid) {
      this.busy = this.adjudicationResource.updateAccessAgreementNote(this.enrollee.id, this.note.value)
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
    this.getEnrollee(this.route.snapshot.params.id);
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

  private getEnrollee(enrolleeId: number) {
    this.busy = this.adjudicationResource.getEnrolleeById(enrolleeId)
      .subscribe((enrollee: HttpEnrollee) => {
        this.enrollee = enrollee;
        if (enrollee.accessAgreementNote) {
          this.note.patchValue(this.enrollee.accessAgreementNote.note);
        }
      });
  }
}
