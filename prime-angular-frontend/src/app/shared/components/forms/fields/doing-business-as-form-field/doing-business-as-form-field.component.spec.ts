import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { UntypedFormControl, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { DoingBusinessAsFormFieldComponent } from './doing-business-as-form-field.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('DoingBusinessAsFormFieldComponent', () => {
  let component: DoingBusinessAsFormFieldComponent;
  let fixture: ComponentFixture<DoingBusinessAsFormFieldComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        DoingBusinessAsFormFieldComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule,
        BrowserAnimationsModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DoingBusinessAsFormFieldComponent);
    component = fixture.componentInstance;
    component.doingBusinessAs = new UntypedFormControl('INPUT_BINDING');
    fixture.detectChanges();
  });

  it('should create', () => {
    component.doingBusinessAs = new UntypedFormControl();
    expect(component).toBeTruthy();
  });
});
