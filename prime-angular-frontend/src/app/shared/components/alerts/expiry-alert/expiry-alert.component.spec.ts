import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpiryAlertComponent } from './expiry-alert.component';

describe('ExpiryAlertComponent', () => {
  let component: ExpiryAlertComponent;
  let fixture: ComponentFixture<ExpiryAlertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ExpiryAlertComponent],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpiryAlertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
