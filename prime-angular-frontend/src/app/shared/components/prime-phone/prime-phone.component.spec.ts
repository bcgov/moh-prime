import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimePhoneComponent } from './prime-phone.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('PrimePhoneComponent', () => {
  let component: PrimePhoneComponent;
  let fixture: ComponentFixture<PrimePhoneComponent>;

  beforeEach(async(() => {
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
        ]
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
