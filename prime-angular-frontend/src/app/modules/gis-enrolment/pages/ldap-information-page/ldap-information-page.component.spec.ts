import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { LdapInformationPageComponent } from './ldap-information-page.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('LdapInformationPageComponent', () => {
  let component: LdapInformationPageComponent;
  let fixture: ComponentFixture<LdapInformationPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        LdapInformationPageComponent,
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [ReactiveFormsModule,
        RouterTestingModule,
        BrowserAnimationsModule,
        NgxMaterialModule],
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
    fixture = TestBed.createComponent(LdapInformationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
