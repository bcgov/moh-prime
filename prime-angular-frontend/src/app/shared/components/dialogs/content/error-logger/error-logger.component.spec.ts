import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogModule } from '@angular/material/dialog';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { ErrorLoggerComponent } from './error-logger.component';

describe('ErrorLoggerComponent', () => {
  let component: ErrorLoggerComponent;
  let fixture: ComponentFixture<ErrorLoggerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        MatDialogModule
      ],
      declarations: [
        ErrorLoggerComponent,
        FormatDatePipe
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: MAT_DIALOG_DATA,
          useValue: {
            data: {
              errorId: 1000
            }
          }
        },
        FormatDatePipe
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ErrorLoggerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
