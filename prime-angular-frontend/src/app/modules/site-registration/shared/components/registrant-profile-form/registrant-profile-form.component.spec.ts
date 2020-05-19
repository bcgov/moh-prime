import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { FormGroup } from '@angular/forms';

import { RegistrantProfileFormComponent } from './registrant-profile-form.component';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';

describe('RegistrantProfileFormComponent', () => {
  let component: RegistrantProfileFormComponent;
  let fixture: ComponentFixture<RegistrantProfileFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SiteRegistrationModule,
        BrowserAnimationsModule,
        RouterTestingModule
      ],
      providers: [
        SiteRegistrationStateService
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrantProfileFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  beforeEach(inject([SiteRegistrationStateService], (siteRegistrationStateService: SiteRegistrationStateService) => {
    fixture = TestBed.createComponent(RegistrantProfileFormComponent);
    component = fixture.componentInstance;
    // Add the bound FormGroup to the component
    component.form = siteRegistrationStateService.signingAuthorityForm as FormGroup;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
