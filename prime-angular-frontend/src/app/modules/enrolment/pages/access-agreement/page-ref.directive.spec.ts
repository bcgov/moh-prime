import { async, TestBed, inject } from '@angular/core/testing';

import { PageRefDirective } from './page-ref.directive';
import { TemplateRef, ElementRef, EmbeddedViewRef } from '@angular/core';

export class MockTemplateRef extends TemplateRef<any> {
  elementRef: ElementRef<any>;
  createEmbeddedView(context: any): EmbeddedViewRef<any> {
    return null;
  }
}

describe('PageRefDirective', () => {
  beforeEach(async(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: TemplateRef,
          useClass: MockTemplateRef
        }
      ]
    }).compileComponents();
  }));

  it('should create an instance', inject([TemplateRef], (templateRef: TemplateRef<any>) => {
    const directive = new PageRefDirective(templateRef);
    expect(directive).toBeTruthy();
  }));
});
