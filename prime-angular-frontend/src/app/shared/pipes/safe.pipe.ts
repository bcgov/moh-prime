import { Pipe, PipeTransform } from '@angular/core';
import { DomSanitizer, SafeHtml, SafeStyle, SafeScript, SafeUrl, SafeResourceUrl } from '@angular/platform-browser';

export type DomSanitizerType = 'html' | 'style' | 'script' | 'url' | 'resourceUrl';

@Pipe({
  name: 'safe'
})
export class SafePipe implements PipeTransform {
  constructor(
    private sanitizer: DomSanitizer
  ) { }

  public transform(value: any, type: DomSanitizerType): SafeHtml | SafeStyle | SafeScript | SafeUrl | SafeResourceUrl {
    if (!value) {
      return value;
    }

    switch (type) {
      case 'html': return this.sanitizer.bypassSecurityTrustHtml(value);
      case 'style': return this.sanitizer.bypassSecurityTrustStyle(value);
      case 'script': return this.sanitizer.bypassSecurityTrustScript(value);
      case 'url': return this.sanitizer.bypassSecurityTrustUrl(value);
      case 'resourceUrl': return this.sanitizer.bypassSecurityTrustResourceUrl(value);
      default: throw new Error(`Invalid DOM sanitizer type specified: ${type}`);
    }
  }
}
