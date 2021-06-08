import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthorityOrganizationInformationPageComponent } from './health-authority-organization-information-page.component';

describe('HealthAuthorityOrganizationInformationPageComponent', () => {
  let component: HealthAuthorityOrganizationInformationPageComponent;
  let fixture: ComponentFixture<HealthAuthorityOrganizationInformationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HealthAuthorityOrganizationInformationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthorityOrganizationInformationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
