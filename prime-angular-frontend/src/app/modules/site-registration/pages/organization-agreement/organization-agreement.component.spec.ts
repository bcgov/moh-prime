import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationAgreementComponent } from './organization-agreement.component';

describe('AccessAgreementComponent', () => {
  let component: OrganizationAgreementComponent;
  let fixture: ComponentFixture<OrganizationAgreementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [OrganizationAgreementComponent]
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
