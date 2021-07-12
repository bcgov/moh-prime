import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LicenseClassesMaintenancePageComponent } from './license-classes-maintenance-page.component';

describe('LicenseClassesMaintenancePageComponent', () => {
  let component: LicenseClassesMaintenancePageComponent;
  let fixture: ComponentFixture<LicenseClassesMaintenancePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LicenseClassesMaintenancePageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LicenseClassesMaintenancePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
