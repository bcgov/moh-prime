import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationToaMaintenanceViewPageComponent } from './organization-toa-maintenance-view-page.component';

describe('OrganizationToaMaintenanceViewPageComponent', () => {
  let component: OrganizationToaMaintenanceViewPageComponent;
  let fixture: ComponentFixture<OrganizationToaMaintenanceViewPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrganizationToaMaintenanceViewPageComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationToaMaintenanceViewPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
