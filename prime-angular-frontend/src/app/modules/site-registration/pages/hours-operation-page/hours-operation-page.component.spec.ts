import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { HoursOperationPageComponent } from './hours-operation-page.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { MatInputModule } from '@angular/material/input';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('HoursOperationPageComponent', () => {
  let component: HoursOperationPageComponent;
  let fixture: ComponentFixture<HoursOperationPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        HoursOperationPageComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [BrowserAnimationsModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule,
        MatInputModule,
        NgxMaskDirective,
        NgxMaskPipe],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideNgxMask(),
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HoursOperationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
