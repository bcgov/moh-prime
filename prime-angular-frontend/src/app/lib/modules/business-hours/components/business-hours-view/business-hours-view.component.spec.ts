import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';

import { BusinessHoursViewComponent } from './business-hours-view.component';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';

describe('BusinessHoursViewComponent', () => {
  let component: BusinessHoursViewComponent;
  let fixture: ComponentFixture<BusinessHoursViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SiteRegistrationModule,
        ReactiveFormsModule
      ],
      providers: [
        // {
        //   provide: APP_CONFIG,
        //   useValue: APP_DI_CONFIG
        // },
        // {
        //   provide: ConfigService,
        //   useClass: MockConfigService
        // },
        SiteRegistrationStateService
      ]
    })
      .compileComponents();
  }));

  beforeEach(inject([SiteRegistrationStateService], (siteRegistrationStateService: SiteRegistrationStateService) => {
    fixture = TestBed.createComponent(BusinessHoursViewComponent);
    component = fixture.componentInstance;
    // Add the bound FormGroup to the component
    component.form = siteRegistrationStateService.hoursOperationForm as FormGroup;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
