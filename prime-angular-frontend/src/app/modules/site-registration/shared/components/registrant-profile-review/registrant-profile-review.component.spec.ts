import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrantProfileReviewComponent } from './registrant-profile-review.component';

describe('RegistrantProfileReviewComponent', () => {
  let component: RegistrantProfileReviewComponent;
  let fixture: ComponentFixture<RegistrantProfileReviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RegistrantProfileReviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrantProfileReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
