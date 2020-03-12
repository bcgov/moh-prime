import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrantProfileFormComponent } from './registrant-profile-form.component';
import { SiteRegistrationModule } from 'app/modules/site-registration/site-registration.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('RegistrantProfileFormComponent', () => {
  let component: RegistrantProfileFormComponent;
  let fixture: ComponentFixture<RegistrantProfileFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SiteRegistrationModule,
        BrowserAnimationsModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegistrantProfileFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
