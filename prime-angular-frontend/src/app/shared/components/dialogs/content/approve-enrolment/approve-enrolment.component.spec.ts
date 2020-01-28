import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveEnrolmentComponent } from './approve-enrolment.component';

describe('ApproveEnrolmentComponent', () => {
  let component: ApproveEnrolmentComponent;
  let fixture: ComponentFixture<ApproveEnrolmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ApproveEnrolmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveEnrolmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
