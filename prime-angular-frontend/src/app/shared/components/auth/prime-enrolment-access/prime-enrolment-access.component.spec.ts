import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppConfig, APP_CONFIG } from 'app/app-config.module';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { PrimeEnrolmentAccessComponent } from './prime-enrolment-access.component';

describe('PrimeEnrolmentAccessComponent', () => {
  let component: PrimeEnrolmentAccessComponent;
  let fixture: ComponentFixture<PrimeEnrolmentAccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PrimeEnrolmentAccessComponent],
      providers: [
        { provide: APP_CONFIG, useValue: AppConfig }
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimeEnrolmentAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
