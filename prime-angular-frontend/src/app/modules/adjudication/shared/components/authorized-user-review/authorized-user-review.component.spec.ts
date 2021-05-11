import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorizedUserReviewComponent } from './authorized-user-review.component';

describe('AuthorizedUserReviewComponent', () => {
  let component: AuthorizedUserReviewComponent;
  let fixture: ComponentFixture<AuthorizedUserReviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AuthorizedUserReviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizedUserReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
