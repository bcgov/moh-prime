import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AbstractControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ActivatedRoute } from '@angular/router';
import { NO_ERRORS_SCHEMA } from '@angular/core';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { DemographicPageComponent } from './demographic-page.component';

describe('DemographicPageComponent', () => {
  let component: DemographicPageComponent;
  let fixture: ComponentFixture<DemographicPageComponent>;
  const mockActivatedRoute = {
    snapshot: {
      data: { title: '' },
      params: { eid: 1 }
    }
  };
  const hasValidator = (control: AbstractControl, validatorFn) => control.hasValidator(validatorFn);

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        NgxMaterialModule,
        ReactiveFormsModule,
        HttpClientTestingModule,
        RouterTestingModule,
        BrowserAnimationsModule
      ],
      declarations: [
        DemographicPageComponent
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        },
        {
          provide: ActivatedRoute,
          useValue: mockActivatedRoute
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DemographicPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should set preferred first and last name as required, but not middle name', () => {
    component.onPreferredNameChange({ checked: false });
    expect(hasValidator(component.formState.preferredFirstName, Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.preferredMiddleName, Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.preferredLastName, Validators.required)).toBeFalsy();

    component.onPreferredNameChange({ checked: true });

    expect(hasValidator(component.formState.preferredFirstName, Validators.required)).toBeTruthy();
    expect(hasValidator(component.formState.preferredMiddleName, Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.preferredLastName, Validators.required)).toBeTruthy();
  });

  it('should clear preferred first and last name validators', () => {
    component.onPreferredNameChange({ checked: true });
    expect(hasValidator(component.formState.preferredFirstName, Validators.required)).toBeTruthy();
    expect(hasValidator(component.formState.preferredMiddleName, Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.preferredLastName, Validators.required)).toBeTruthy();

    component.onPreferredNameChange({ checked: false });

    expect(hasValidator(component.formState.preferredFirstName, Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.preferredMiddleName, Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.preferredLastName, Validators.required)).toBeFalsy();
  });

  it('should set physical address as required, but not id or street2', () => {
    component.onPhysicalAddressChange({ checked: false });
    expect(hasValidator(component.formState.physicalAddress, Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('countryCode'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('provinceCode'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('street'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('street2'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('city'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('postal'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('id'), Validators.required)).toBeFalsy();

    component.onPhysicalAddressChange({ checked: true });

    expect(hasValidator(component.formState.physicalAddress.get('countryCode'), Validators.required)).toBeTruthy();
    expect(hasValidator(component.formState.physicalAddress.get('provinceCode'), Validators.required)).toBeTruthy();
    expect(hasValidator(component.formState.physicalAddress.get('street'), Validators.required)).toBeTruthy();
    expect(hasValidator(component.formState.physicalAddress.get('street2'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('city'), Validators.required)).toBeTruthy();
    expect(hasValidator(component.formState.physicalAddress.get('postal'), Validators.required)).toBeTruthy();
    expect(hasValidator(component.formState.physicalAddress.get('id'), Validators.required)).toBeFalsy();
  });

  it('should clear physical address validators', () => {
    component.onPhysicalAddressChange({ checked: true });
    expect(hasValidator(component.formState.physicalAddress.get('countryCode'), Validators.required)).toBeTruthy();
    expect(hasValidator(component.formState.physicalAddress.get('provinceCode'), Validators.required)).toBeTruthy();
    expect(hasValidator(component.formState.physicalAddress.get('street'), Validators.required)).toBeTruthy();
    expect(hasValidator(component.formState.physicalAddress.get('street2'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('city'), Validators.required)).toBeTruthy();
    expect(hasValidator(component.formState.physicalAddress.get('postal'), Validators.required)).toBeTruthy();
    expect(hasValidator(component.formState.physicalAddress.get('id'), Validators.required)).toBeFalsy();

    component.onPhysicalAddressChange({ checked: false });

    expect(hasValidator(component.formState.physicalAddress.get('countryCode'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('provinceCode'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('street'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('street2'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('city'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('postal'), Validators.required)).toBeFalsy();
    expect(hasValidator(component.formState.physicalAddress.get('id'), Validators.required)).toBeFalsy();
  });
});
