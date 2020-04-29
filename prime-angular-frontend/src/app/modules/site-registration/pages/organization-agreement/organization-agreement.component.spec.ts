import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { OrganizationAgreementComponent } from './organization-agreement.component';
import { SiteRegistrationModule } from '@registration/site-registration.module';

describe('OrganizationAgreementComponent', () => {
  let component: OrganizationAgreementComponent;
  let fixture: ComponentFixture<OrganizationAgreementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        SiteRegistrationModule
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
