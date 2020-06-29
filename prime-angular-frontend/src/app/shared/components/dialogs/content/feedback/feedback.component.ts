import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';

import { debounceTime } from 'rxjs/operators';

import { Feedback } from '@shared/models/feedback.model';

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.scss']
})
export class FeedbackComponent implements OnInit {
  public form: FormGroup;
  public feedback: Feedback;
  @Output() output = new EventEmitter<Feedback>();

  constructor(
    private fb: FormBuilder,
  ) { }

  public get comment(): FormControl {
    return this.form.get('comment') as FormControl;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      comment: [
        '',
        []
      ]
    });
  }

  private initForm() {
    this.feedback = new Feedback();
    this.updateDialogContentOutput(this.feedback);

    this.comment.valueChanges
      .pipe(debounceTime(250))
      .subscribe((comment: string) => {
        this.feedback.comment = comment;
        this.updateDialogContentOutput(this.feedback);
      });
  }

  private updateDialogContentOutput(feedback: Feedback) {
    this.output.emit(feedback);
  }
}
