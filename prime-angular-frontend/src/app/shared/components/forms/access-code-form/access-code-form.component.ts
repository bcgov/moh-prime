import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormGroup, UntypedFormControl } from '@angular/forms';

@Component({
  selector: 'app-access-code-form',
  templateUrl: './access-code-form.component.html',
  styleUrls: ['./access-code-form.component.scss']
})
export class AccessCodeFormComponent implements OnInit {
  @Input() public form: UntypedFormGroup;

  constructor() { }

  public get accessCode(): UntypedFormControl {
    return this.form.get('accessCode') as UntypedFormControl;
  }

  public ngOnInit(): void { }
}
