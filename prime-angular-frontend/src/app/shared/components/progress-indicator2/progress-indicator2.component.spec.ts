import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProgressIndicator2Component } from './progress-indicator2.component';

describe('ProgressIndicator2Component', () => {
  let component: ProgressIndicator2Component;
  let fixture: ComponentFixture<ProgressIndicator2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProgressIndicator2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProgressIndicator2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
