import { Directive, ViewContainerRef } from '@angular/core';

@Directive({
    selector: '[appDialogContent]',
    standalone: false
})
export class DialogContentDirective {
  constructor(
    public viewContainerRef: ViewContainerRef
  ) { }
}
