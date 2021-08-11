import { NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { LdapInformationPageComponent } from './ldap-information-page.component';

xdescribe('LdapInformationPageComponent', () => {
  let component: LdapInformationPageComponent;
  let fixture: ComponentFixture<LdapInformationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [
        LdapInformationPageComponent,
      ],
      imports: [
        ReactiveFormsModule,
        RouterTestingModule,
        HttpClientTestingModule,
        BrowserAnimationsModule,
        NgxMaterialModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LdapInformationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
