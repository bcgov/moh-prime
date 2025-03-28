import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UntypedFormGroup, UntypedFormControl, UntypedFormBuilder } from '@angular/forms';

import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.scss']
})
export class NoteComponent implements OnInit {
  public form: UntypedFormGroup;
  public isEmpty: boolean;
  @Output() public output: EventEmitter<string>;

  constructor(
    private fb: UntypedFormBuilder,
  ) {
    this.output = new EventEmitter<string>();
  }

  public get note(): UntypedFormControl {
    return this.form.get('note') as UntypedFormControl;
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      note: [
        {
          value: '',
          disabled: false,
        },
        []
      ]
    });
  }

  protected initForm() {
    this.note.valueChanges
      .pipe(
        debounceTime(250)
      )
      .subscribe((note: string) => this.output.emit(note));
  }
}
