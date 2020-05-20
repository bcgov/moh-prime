import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';

import { BusinessHoursPickerComponent } from './business-hours-picker.component';
import { SiteRegistrationModule } from '@registration/site-registration.module';

describe('BusinessHoursPickerComponent', () => {
  let component: BusinessHoursPickerComponent;
  let fixture: ComponentFixture<BusinessHoursPickerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SiteRegistrationModule,
        BrowserAnimationsModule,
        ReactiveFormsModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BusinessHoursPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
