import { Directive } from '@angular/core';

@Directive({
    selector: '[appContextualContent]',
    standalone: false
})
export class ContextualContentDirective {
  constructor() { }
}
