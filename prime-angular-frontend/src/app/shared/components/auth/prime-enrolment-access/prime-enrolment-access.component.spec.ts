import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { MatDialogModule } from '@angular/material/dialog';
import { RouterTestingModule } from '@angular/router/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { PrimeEnrolmentAccessComponent } from './prime-enrolment-access.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('PrimeEnrolmentAccessComponent', () => {
  let component: PrimeEnrolmentAccessComponent;
  let fixture: ComponentFixture<PrimeEnrolmentAccessComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [PrimeEnrolmentAccessComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        MatDialogModule,
        ReactiveFormsModule,
        NgxMaterialModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        CapitalizePipe,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimeEnrolmentAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
