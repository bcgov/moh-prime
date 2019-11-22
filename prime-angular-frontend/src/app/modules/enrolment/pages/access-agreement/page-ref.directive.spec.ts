import { inject } from '@angular/core/testing';

import { PageRefDirective } from './page-ref.directive';
import { TemplateRef } from '@angular/core';

describe('PageRefDirective', () => {
  it('should create an instance', inject([TemplateRef], (templateRef: TemplateRef<any>) => {
    const directive = new PageRefDirective(templateRef);
    expect(directive).toBeTruthy();
  }));
});
