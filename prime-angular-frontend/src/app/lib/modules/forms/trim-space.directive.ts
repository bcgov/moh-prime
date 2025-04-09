import { DefaultValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import {
  Directive,
  ElementRef,
  HostListener,
  Renderer2,
  forwardRef,
} from '@angular/core';

@Directive({
  selector: 'input[appTrimSpace]',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      multi: true,
      useExisting: forwardRef(() => TrimSpaceInputDirective),
    },
  ],
})

export class TrimSpaceInputDirective extends DefaultValueAccessor {
  @HostListener('input', ['$event']) input($event: InputEvent) {
    const target = $event.target as HTMLInputElement;
    const start = target.selectionStart;

    target.value = target.value.trim();
    target.setSelectionRange(start, start);
    this.onChange(target.value);
  }

  constructor(renderer: Renderer2, elementRef: ElementRef) {
    super(renderer, elementRef, false);
  }
}
