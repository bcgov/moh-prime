import { provideHttpClientTesting } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { EscalationNoteComponent } from './escalation-note.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('EscalationNoteComponent', () => {
  let component: EscalationNoteComponent;
  let fixture: ComponentFixture<EscalationNoteComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [EscalationNoteComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [NgxMaterialModule,
        ReactiveFormsModule,
        BrowserAnimationsModule],
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
            useValue: {
                data: {
                    id: 1,
                    escalationType: 1
                }
            }
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting(),
    ]
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EscalationNoteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
