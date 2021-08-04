import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationClaimConfirmationPageComponent } from './organization-claim-confirmation-page.component';

describe('OrganizationClaimConfirmationPageComponent', () => {
  let component: OrganizationClaimConfirmationPageComponent;
  let fixture: ComponentFixture<OrganizationClaimConfirmationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrganizationClaimConfirmationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationClaimConfirmationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
