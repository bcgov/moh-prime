import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimePhoneComponent } from './prime-phone.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('PrimePhoneComponent', () => {
  let component: PrimePhoneComponent;
  let fixture: ComponentFixture<PrimePhoneComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          PrimePhoneComponent
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimePhoneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
