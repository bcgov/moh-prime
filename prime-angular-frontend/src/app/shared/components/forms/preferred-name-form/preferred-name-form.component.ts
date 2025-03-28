import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormGroup } from '@angular/forms';

@Component({
  selector: 'app-preferred-name-form',
  templateUrl: './preferred-name-form.component.html',
  styleUrls: ['./preferred-name-form.component.scss']
})
export class PreferredNameFormComponent implements OnInit {
  /**
   * @description
   * Preferred name form.
   */
  @Input() public form: UntypedFormGroup;
  /**
   * @description
   * Mode for displaying the form fields as a
   * single or multiple columns.
   */
  @Input() public mode: 'column' | 'columns';

  public formControlConfig: { label: string, name: string }[];

  constructor() {
    this.mode = 'column';
    this.formControlConfig = [
      { label: 'Alternate First Name', name: 'preferredFirstName' },
      { label: 'Alternate Middle Name (Optional)', name: 'preferredMiddleName' },
      { label: 'Alternate Last Name', name: 'preferredLastName' }
    ];
  }

  public ngOnInit(): void { }
}
