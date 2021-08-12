import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ManualFlagNoteComponent } from './manual-flag-note.component';

describe('ManualFlagNoteComponent', () => {
  let component: ManualFlagNoteComponent;
  let fixture: ComponentFixture<ManualFlagNoteComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        BrowserAnimationsModule
      ],
      declarations: [],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: MatDialogRef,
          useValue: {
            close: (dialogResult: any) => { }
          }
        },
        {
          provide: MAT_DIALOG_DATA,
          useValue: {}
        },
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ManualFlagNoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
