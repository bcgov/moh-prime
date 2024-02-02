import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { MatDialogModule } from '@angular/material/dialog';
import { RouterTestingModule } from '@angular/router/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { PrimeEnrolmentAccessComponent } from './prime-enrolment-access.component';
import { ReactiveFormsModule } from '@angular/forms';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';

describe('PrimeEnrolmentAccessComponent', () => {
  let component: PrimeEnrolmentAccessComponent;
  let fixture: ComponentFixture<PrimeEnrolmentAccessComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        MatDialogModule,
        ReactiveFormsModule,
        NgxMaterialModule,
        HttpClientTestingModule,
      ],
      declarations: [PrimeEnrolmentAccessComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        CapitalizePipe
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
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
