import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessLicenceExpiryComponent } from './business-licence-expiry.component';

describe('BusinessLicenceComponent', () => {
  let component: BusinessLicenceExpiryComponent;
  let fixture: ComponentFixture<BusinessLicenceExpiryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [BusinessLicenceExpiryComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BusinessLicenceExpiryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
