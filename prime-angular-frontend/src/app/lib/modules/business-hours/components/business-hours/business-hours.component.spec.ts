import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';

import { BusinessHoursComponent } from './business-hours.component';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';

describe('BusinessHoursComponent', () => {
  let component: BusinessHoursComponent;
  let fixture: ComponentFixture<BusinessHoursComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SiteRegistrationModule,
        BrowserAnimationsModule,
        RouterTestingModule,
        ReactiveFormsModule
      ],
      providers: [
        SiteFormStateService
      ]
    }).compileComponents();
  }));

  beforeEach(inject([SiteFormStateService], (siteFormStateService: SiteFormStateService) => {
    fixture = TestBed.createComponent(BusinessHoursComponent);
    component = fixture.componentInstance;
    // Add the bound FormGroup to the component
    component.form = siteFormStateService.hoursOperationForm as FormGroup;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
