import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrganizationToaMaintenanceListPageComponent } from './organization-toa-maintenance-list-page.component';

describe('EnrolleeToaMaintenanceListPageComponent', () => {
  let component: OrganizationToaMaintenanceListPageComponent;
  let fixture: ComponentFixture<OrganizationToaMaintenanceListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [OrganizationToaMaintenanceListPageComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationToaMaintenanceListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

