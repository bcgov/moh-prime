import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationAgreementPageComponent } from './organization-agreement-page.component';

describe('OrganizationAgreementPageComponent', () => {
  let component: OrganizationAgreementPageComponent;
  let fixture: ComponentFixture<OrganizationAgreementPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrganizationAgreementPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationAgreementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
