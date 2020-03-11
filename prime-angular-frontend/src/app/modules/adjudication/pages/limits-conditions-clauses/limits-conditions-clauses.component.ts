import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { HttpEnrollee } from '@shared/models/enrolment.model';

import { AuthService } from '@auth/shared/services/auth.service';
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

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private adjudicationResource: AdjudicationResource,
    private authService: AuthService
  ) {
    this.hasActions = false;
  }

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
  }

  /**
   * Updates the preview with the editor content
   */
  public handleChange(event: { editor: any }) {
    if (!event.editor) { return; }
    this.preview = event.editor.getContent();
  }

  public onSubmit() {
    if (this.form.valid) {
      this.busy = this.adjudicationResource.updateAccessAgreementNote(this.enrollee.id, this.note.value)
        .subscribe();
    }
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
    this.getEnrollee(this.route.snapshot.params.id);
  }

  protected initForm() {
    this.note.valueChanges.subscribe((value: string) => this.preview = value);
  }

  protected createFormInstance() {
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

  private getEnrollee(enrolleeId: number, statusCode?: number) {
    this.busy = this.adjudicationResource.getEnrolleeById(enrolleeId, statusCode)
      .subscribe((enrollee: HttpEnrollee) => {
        this.enrollee = enrollee;
        if (enrollee.accessAgreementNote) {
          this.note.patchValue(this.enrollee.accessAgreementNote.note);
        }
      });
  }
}
