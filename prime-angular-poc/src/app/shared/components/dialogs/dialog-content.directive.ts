import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
  selector: '[appDialogContent]'
})
export class DialogContentDirective {
  constructor(
    public viewContainerRef: ViewContainerRef
  ) { }
}
