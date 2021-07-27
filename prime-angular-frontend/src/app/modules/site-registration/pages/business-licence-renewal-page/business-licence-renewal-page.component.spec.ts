import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessLicenceRenewalPageComponent } from './business-licence-renewal-page.component';

describe('BusinessLicenceRenewalPageComponent', () => {
  let component: BusinessLicenceRenewalPageComponent;
  let fixture: ComponentFixture<BusinessLicenceRenewalPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BusinessLicenceRenewalPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BusinessLicenceRenewalPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
