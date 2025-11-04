import { ComponentFixture, TestBed, inject, waitForAsync } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule, UntypedFormControl, UntypedFormGroup } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockOrganizationService } from 'test/mocks/mock-organization.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { AuthorizedUserPageComponent } from './authorized-user-page.component';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';

describe('AuthorizedUserPageComponent', () => {
  let component: AuthorizedUserPageComponent;
  let fixture: ComponentFixture<AuthorizedUserPageComponent>;
  let spyOnTogglePreferredNameValidators;
  let spyOnFormGetPreferredMiddleName;
  let spyOnToggleAddressLineValidators;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule,
        BrowserAnimationsModule,
        NgxMaskDirective,
        NgxMaskPipe
      ],
      declarations: [
        AuthorizedUserPageComponent
      ],
      providers: [
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        },
        {
          provide: OrganizationService,
          useClass: MockOrganizationService
        },
        OrganizationFormStateService,
        CapitalizePipe,
        provideNgxMask()
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(inject(
    [OrganizationService, OrganizationFormStateService],
    (organizationService: OrganizationService, organizationFormStateService: OrganizationFormStateService) => {
      fixture = TestBed.createComponent(AuthorizedUserPageComponent);
      component = fixture.componentInstance;
      organizationFormStateService.setForm(organizationService.organization);
      fixture.detectChanges();
    })
  );

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing onPreferredNameChange()', () => {
    beforeEach(() => {
      spyOnTogglePreferredNameValidators = spyOn<any>(component, 'togglePreferredNameValidators');
      spyOnFormGetPreferredMiddleName = spyOn<any>(component.formState.form.get('preferredMiddleName'), 'reset');
    });

    it('should not call getPreferredMiddleName and call togglePreferredNameValidators with true as first param', () => {
      component.hasPreferredName = true;
      const checked = true;

      component.onPreferredNameChange({ checked });
      expect(spyOnFormGetPreferredMiddleName).toHaveBeenCalledTimes(0);
      expect(spyOnTogglePreferredNameValidators).toHaveBeenCalled();
      expect(spyOnTogglePreferredNameValidators).toHaveBeenCalledWith(checked, jasmine.any(UntypedFormControl), jasmine.any(UntypedFormControl));
    });

    it('should call getPreferredMiddleName once and call togglePreferredNameValidators with false as first param', () => {
      component.hasPreferredName = false;
      const checked = false;

      component.onPreferredNameChange({ checked });
      expect(spyOnFormGetPreferredMiddleName).toHaveBeenCalledTimes(1);
      expect(spyOnTogglePreferredNameValidators).toHaveBeenCalled();
      expect(spyOnTogglePreferredNameValidators).toHaveBeenCalledWith(checked, jasmine.any(UntypedFormControl), jasmine.any(UntypedFormControl));
    });
  });

  describe('testing onPhysicalAddressChange()', () => {
    beforeEach(() => {
      spyOnToggleAddressLineValidators = spyOn<any>(component, 'toggleAddressLineValidators');
    });

    it('should call onPhysicalAddressChange with true as the first argument', () => {
      const checked = true;

      component.onPhysicalAddressChange({ checked });
      expect(spyOnToggleAddressLineValidators).toHaveBeenCalledWith(checked, jasmine.any(UntypedFormGroup));
    });

    it('should call onPhysicalAddressChange with false as the first argument', () => {
      const checked = false;

      component.onPhysicalAddressChange({ checked });
      expect(spyOnToggleAddressLineValidators).toHaveBeenCalledWith(checked, jasmine.any(UntypedFormGroup));
    });
  });
});
