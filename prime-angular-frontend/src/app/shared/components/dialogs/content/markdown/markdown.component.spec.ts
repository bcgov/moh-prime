import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MarkdownComponent } from './markdown.component';
import { MarkdownModule, MarkdownService, MarkedOptions } from 'ngx-markdown';

describe('MarkdownComponent', () => {
  let component: MarkdownComponent;
  let fixture: ComponentFixture<MarkdownComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          MarkdownModule
        ],
        declarations: [
          MarkdownComponent
        ],
        providers: [
          MarkdownService,
          MarkedOptions
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MarkdownComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
