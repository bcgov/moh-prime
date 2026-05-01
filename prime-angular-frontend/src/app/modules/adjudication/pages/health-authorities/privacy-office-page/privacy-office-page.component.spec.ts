import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { MatDialogModule } from '@angular/material/dialog';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { PrivacyOfficePageComponent } from './privacy-office-page.component';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('PrivacyOfficePageComponent', () => {
  let component: PrivacyOfficePageComponent;
  let fixture: ComponentFixture<PrivacyOfficePageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        PrivacyOfficePageComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        ReactiveFormsModule,
        MatDialogModule,
        MatSnackBarModule,
        NgxMaskDirective,
        NgxMaskPipe],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        CapitalizePipe,
        provideNgxMask(),
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrivacyOfficePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
