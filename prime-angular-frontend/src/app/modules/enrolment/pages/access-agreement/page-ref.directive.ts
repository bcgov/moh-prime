import { Directive, TemplateRef } from '@angular/core';

@Directive({
  selector: '[appPageRef]'
})
export class PageRefDirective {
  constructor(
    public templateRef: TemplateRef<any>
  ) { }
}
