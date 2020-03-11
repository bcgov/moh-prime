import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeReviewStatusComponent } from './enrollee-review-status.component';

describe('EnrolleeReviewStatusComponent', () => {
  let component: EnrolleeReviewStatusComponent;
  let fixture: ComponentFixture<EnrolleeReviewStatusComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolleeReviewStatusComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeReviewStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
