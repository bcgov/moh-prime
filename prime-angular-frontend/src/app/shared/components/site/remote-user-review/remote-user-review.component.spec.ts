import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';

import { RemoteUserReviewComponent } from './remote-user-review.component';

describe('RemoteUserReviewComponent', () => {
  let component: RemoteUserReviewComponent;
  let fixture: ComponentFixture<RemoteUserReviewComponent>;

  beforeEach(async() => {
    await TestBed.configureTestingModule({
      declarations: [
        RemoteUserReviewComponent
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoteUserReviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
