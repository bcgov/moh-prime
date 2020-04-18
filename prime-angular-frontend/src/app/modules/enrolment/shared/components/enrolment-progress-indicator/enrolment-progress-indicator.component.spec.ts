import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolmentProgressIndicatorComponent } from './enrolment-progress-indicator.component';

describe('EnrolmentProgressIndicatorComponent', () => {
  let component: EnrolmentProgressIndicatorComponent;
  let fixture: ComponentFixture<EnrolmentProgressIndicatorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolmentProgressIndicatorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolmentProgressIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
