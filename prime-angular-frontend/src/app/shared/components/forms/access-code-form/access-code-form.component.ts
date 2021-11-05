import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-access-code-form',
  templateUrl: './access-code-form.component.html',
  styleUrls: ['./access-code-form.component.scss']
})
export class AccessCodeFormComponent implements OnInit {
  @Input() public form: FormGroup;

  constructor() { }

  public get accessCode(): FormControl {
    return this.form.get('accessCode') as FormControl;
  }

  public ngOnInit(): void { }
}
