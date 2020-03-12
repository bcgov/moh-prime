import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrivacyOfficerComponent } from './privacy-officer.component';
import { SiteRegistrationModule } from '../../site-registration.module';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

describe('PrivacyOfficerComponent', () => {
  let component: PrivacyOfficerComponent;
  let fixture: ComponentFixture<PrivacyOfficerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SiteRegistrationModule,
        RouterTestingModule,
        BrowserAnimationsModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrivacyOfficerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
