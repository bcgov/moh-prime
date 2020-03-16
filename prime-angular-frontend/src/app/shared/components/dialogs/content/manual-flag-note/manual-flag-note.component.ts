import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { debounceTime } from 'rxjs/operators';
import { Enrolment } from '@shared/models/enrolment.model';

export class ManualFlagNoteOutput {
  public note: string;
  public alwaysManual: boolean;
}

@Component({
  selector: 'app-manual-flag-note',
  templateUrl: './manual-flag-note.component.html',
  styleUrls: ['./manual-flag-note.component.scss']
})
export class ManualFlagNoteComponent implements OnInit {

  public form: FormGroup;
  public isEmpty: boolean;
  public enrolment: Enrolment;
  @Output() output = new EventEmitter<ManualFlagNoteOutput>();
  public outputValue: ManualFlagNoteOutput;

  constructor(
    private fb: FormBuilder,
  ) { }

  @Input()
  public set data({ enrolment }: { enrolment: Enrolment }) {
    this.enrolment = enrolment;
  }

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
  }

  public get alwaysManual(): FormControl {
    return this.form.get('alwaysManual') as FormControl;
  }

  ngOnInit() {
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
      ],
      alwaysManual: [
        {
          value: this.enrolment ? this.enrolment.alwaysManual : false,
          disabled: false,
        },
        []
      ]
    });
  }

  protected initForm() {
    this.outputValue = new ManualFlagNoteOutput();
    this.outputValue.note = this.note.value;
    this.outputValue.alwaysManual = this.alwaysManual.value;

    this.note.valueChanges.pipe(
      debounceTime(250),
    ).subscribe((note: string) => this.outputValue.note = note);
    this.alwaysManual.valueChanges.pipe(
      debounceTime(250)
    ).subscribe((flag: boolean) => this.outputValue.alwaysManual = flag);

    this.output.emit(this.outputValue);
  }

}
