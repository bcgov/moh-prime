import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';

/**
 * @deprecated replaced by SafePipe to apply all DomSanitizer types
 */
@Pipe({
  name: 'safeHtml'
})
export class SafeHtmlPipe implements PipeTransform {
  constructor(private sanitizer: DomSanitizer) { }

  transform(html: string): any {
    return (html)
      ? this.sanitizer.bypassSecurityTrustHtml(html)
      : null;
  }
}
