import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LinkSiteComponent } from './link-site.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { HttpClientModule } from '@angular/common/http';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('LinkSiteComponent', () => {
  let component: LinkSiteComponent;
  let fixture: ComponentFixture<LinkSiteComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        BrowserAnimationsModule,
        MatSnackBarModule,
        HttpClientModule
      ],
      declarations: [LinkSiteComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: MatDialogRef,
          useValue: {
            close: (dialogResult: any) => { },
            updateSize: () => { }
          }
        },
        {
          provide: MAT_DIALOG_DATA,
          useValue: {
            data: {
            }
          }
        },
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
    fixture = TestBed.createComponent(LinkSiteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
