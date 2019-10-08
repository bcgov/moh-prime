import { Component, OnInit, forwardRef, OnDestroy, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators, NG_VALUE_ACCESSOR, NG_VALIDATORS, ControlValueAccessor } from '@angular/forms';

import { Subscription } from 'rxjs';

import { Login } from '@auth/shared/models/login.model';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => LoginFormComponent),
      multi: true
    },
    {
      provide: NG_VALIDATORS,
      useExisting: forwardRef(() => LoginFormComponent),
      multi: true
    }
  ]
})
export class LoginFormComponent implements ControlValueAccessor, OnInit, OnChanges, OnDestroy {
  // TODO: find a better way to do this without passing an Input(), and using OnChanges
  @Input() public submitted: boolean;

  public form: FormGroup;
  public subscriptions: Subscription[] = [];
  public maskPassword = true;

  public onChange: any = () => { };
  public onTouched: any = () => { };

  constructor(
    private fb: FormBuilder
  ) { }

  public get username(): FormControl {
    return this.form.get('username') as FormControl;
  }

  public get password(): FormControl {
    return this.form.get('password') as FormControl;
  }

  public get value(): Login {
    return this.form.value;
  }

  public set value(value: Login) {
    this.form.setValue(value, { emitEvent: false });
    this.onChange(value);
    this.onTouched();
  }

  public registerOnChange(fn) {
    this.onChange = fn;
  }

  public writeValue(value) {
    if (value) {
      this.value = value;
    }
  }

  public registerOnTouched(fn) {
    this.onTouched = fn;
  }

  public validate(_: FormControl) {
    return (this.form.valid) ? null : { login: { valid: false } };
  }

  public ngOnInit() {
    this.createFormInstance();

    this.subscriptions.push(
      this.form.valueChanges.subscribe(value => {
        this.onChange(value);
        this.onTouched();
      })
    );
  }

  public ngOnChanges(changes: SimpleChanges) {
    // Display input errors when the form is submitted
    if (changes.submitted.currentValue) {
      this.form.markAllAsTouched();
    }
  }

  public ngOnDestroy() {
    this.subscriptions.forEach(s => s.unsubscribe());
  }

  private createFormInstance() {
    this.form = this.fb.group({
      username: [
        '',
        [Validators.required]
      ],
      password: [
        '',
        [Validators.required]
      ]
    }, {
      updateOn: 'blur'
    });
  }

}
