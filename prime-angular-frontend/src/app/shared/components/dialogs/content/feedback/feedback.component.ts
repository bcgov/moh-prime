import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { debounceTime } from 'rxjs/operators';

export class Feedback {
  public satisfied: boolean;
  public comment: string;
  public enrolleeId: number;
  public route: string;
}

@Component({
  selector: 'app-feedback',
  templateUrl: './feedback.component.html',
  styleUrls: ['./feedback.component.scss']
})
export class FeedbackComponent implements OnInit {
  public form: FormGroup;
  @Output() output = new EventEmitter<Feedback>();
  public feedback: Feedback;

  constructor(
    private fb: FormBuilder,
  ) { }

  public get comment(): FormControl {
    return this.form.get('comment') as FormControl;
  }

  public onSatisfiedChange(satisfied: boolean): void {
    this.feedback.satisfied = satisfied;
  }

  ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      comment: [
        {
          value: '',
          disabled: false,
        },
        []
      ]
    });
  }

  protected initForm() {
    this.feedback = new Feedback();

    this.comment.valueChanges.pipe(
      debounceTime(250)
    ).subscribe((comment: string) => this.feedback.comment = comment);

    this.output.emit(this.feedback);
  }

}
