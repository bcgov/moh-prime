import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { TechnicalSupportsPageComponent } from './technical-supports-page.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('TechnicalSupportsPageComponent', () => {
  let component: TechnicalSupportsPageComponent;
  let fixture: ComponentFixture<TechnicalSupportsPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        TechnicalSupportsPageComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [ReactiveFormsModule,
        RouterTestingModule,
        MatDialogModule,
        MatSnackBarModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        CapitalizePipe,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TechnicalSupportsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
