import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { PrimeSupportEmailComponent } from './prime-support-email.component';

describe('PrimeSupportEmailComponent', () => {
  let component: PrimeSupportEmailComponent;
  let fixture: ComponentFixture<PrimeSupportEmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        PrimeSupportEmailComponent
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimeSupportEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
