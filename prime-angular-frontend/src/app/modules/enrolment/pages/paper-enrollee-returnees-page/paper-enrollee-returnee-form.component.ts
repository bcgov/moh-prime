import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-paper-enrollee-returnee-form',
  template: `
    <div [formGroup]="form">
      <div *ngFor="let control of formControlConfig"
          class="row">
          <div [class.col-12]="mode === 'column'"
               [class.col-6]="mode === 'columns'">
          <mat-form-field class="w-100">
            <input matInput
                  [placeholder]="control.label"
                  [formControlName]="control.name">
            <mat-error>Required</mat-error>
          </mat-form-field>
      </div>
    </div>
  `,
  styles: ['']
})
export class PaperEnrolleeReturneeFormComponent implements OnInit {
  @Input() public form: FormGroup;
  public formControlConfig: { label: string, name: string }[];
  @Input() public mode: 'column' | 'columns';


  constructor() {
    this.mode = 'column';

    this.formControlConfig = [
      { label: 'GPID', name: 'gpid' }
    ];
  }

  ngOnInit(): void { }

}
