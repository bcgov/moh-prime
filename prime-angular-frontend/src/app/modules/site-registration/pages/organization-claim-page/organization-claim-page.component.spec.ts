import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationClaimPageComponent } from './organization-claim-page.component';

describe('OrganizationClaimPageComponent', () => {
  let component: OrganizationClaimPageComponent;
  let fixture: ComponentFixture<OrganizationClaimPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrganizationClaimPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationClaimPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
