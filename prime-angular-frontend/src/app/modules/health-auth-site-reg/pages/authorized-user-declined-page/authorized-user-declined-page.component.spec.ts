import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AuthorizedUserDeclinedPageComponent } from './authorized-user-declined-page.component';

describe('AuthorizedUserDeclinedPageComponent', () => {
  let component: AuthorizedUserDeclinedPageComponent;
  let fixture: ComponentFixture<AuthorizedUserDeclinedPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AuthorizedUserDeclinedPageComponent],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AuthorizedUserDeclinedPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
