import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';

import { BusinessHoursComponent } from './business-hours.component';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';
import { SiteRegistrationModule } from '@registration/site-registration.module';

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
        SiteRegistrationStateService
      ]
    }).compileComponents();
  }));

  beforeEach(inject([SiteRegistrationStateService], (siteRegistrationStateService: SiteRegistrationStateService) => {
    fixture = TestBed.createComponent(BusinessHoursComponent);
    component = fixture.componentInstance;
    // Add the bound FormGroup to the component
    component.form = siteRegistrationStateService.hoursOperationForm as FormGroup;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
