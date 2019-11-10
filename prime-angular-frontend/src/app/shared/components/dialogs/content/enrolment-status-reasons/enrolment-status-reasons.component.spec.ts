import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolmentStatusReasonsComponent } from './enrolment-status-reasons.component';

describe('EnrolmentStatusReasonsComponent', () => {
  let component: EnrolmentStatusReasonsComponent;
  let fixture: ComponentFixture<EnrolmentStatusReasonsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          EnrolmentStatusReasonsComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolmentStatusReasonsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
