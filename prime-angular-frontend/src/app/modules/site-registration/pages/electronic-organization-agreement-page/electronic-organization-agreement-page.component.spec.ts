import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ElectronicOrganizationAgreementPageComponent } from './electronic-organization-agreement-page.component';

describe('ElectronicOrganizationAgreementPageComponent', () => {
  let component: ElectronicOrganizationAgreementPageComponent;
  let fixture: ComponentFixture<ElectronicOrganizationAgreementPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ElectronicOrganizationAgreementPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ElectronicOrganizationAgreementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
