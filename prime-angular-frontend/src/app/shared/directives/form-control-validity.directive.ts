import { Directive, Self, HostBinding, Input } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
  selector: '[appFormControlValidity]'
})
export class FormControlValidityDirective {
  constructor(
    @Self() private ngControl: NgControl
  ) { }

  @HostBinding('class.is-valid')
  public get isValid(): boolean {
    return this.valid;
  }

  @HostBinding('class.is-invalid')
  public get isInvalid(): boolean {
    return this.invalid;
  }

  public get valid(): boolean {
    return this.ngControl.valid &&
      (this.ngControl.dirty || this.ngControl.touched);
  }

  public get invalid(): boolean {
    return !this.ngControl.pending &&
      !this.ngControl.valid &&
      (this.ngControl.touched || this.ngControl.dirty);
  }
}
