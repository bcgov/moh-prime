import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';

import { debounceTime } from 'rxjs/operators';

@Component({
  selector: 'app-note',
  templateUrl: './note.component.html',
  styleUrls: ['./note.component.scss']
})
export class NoteComponent implements OnInit {
  public form: FormGroup;
  public isEmpty: boolean;
  @Output() public output: EventEmitter<string>;

  constructor(
    private fb: FormBuilder,
  ) { 
      this.output = new EventEmitter<string>();
  }

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
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
