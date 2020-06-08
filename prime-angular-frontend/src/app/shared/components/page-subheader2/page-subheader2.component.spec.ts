import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PageSubheader2Component } from './page-subheader2.component';

describe('PageSubheader2Component', () => {
  let component: PageSubheader2Component;
  let fixture: ComponentFixture<PageSubheader2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PageSubheader2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PageSubheader2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
