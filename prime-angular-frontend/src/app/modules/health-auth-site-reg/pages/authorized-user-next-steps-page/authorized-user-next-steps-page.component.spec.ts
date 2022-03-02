import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AuthorizedUserNextStepsPageComponent } from './authorized-user-next-steps-page.component';

describe('AuthorizedUserNextStepsPageComponent', () => {
  let component: AuthorizedUserNextStepsPageComponent;
  let fixture: ComponentFixture<AuthorizedUserNextStepsPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [AuthorizedUserNextStepsPageComponent],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizedUserNextStepsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
