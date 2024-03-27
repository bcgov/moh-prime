import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AuthorizedUserDisabledPageComponent } from './authorized-user-disabled-page.component';

describe('AuthorizedUserDisabledPageComponent', () => {
  let component: AuthorizedUserDisabledPageComponent;
  let fixture: ComponentFixture<AuthorizedUserDisabledPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [AuthorizedUserDisabledPageComponent],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizedUserDisabledPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
