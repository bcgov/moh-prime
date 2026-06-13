import { Directive } from '@angular/core';

@Directive({
    selector: '[appContextualTitle]',
    standalone: false
})
export class ContextualTitleDirective {
  constructor() { }
}
