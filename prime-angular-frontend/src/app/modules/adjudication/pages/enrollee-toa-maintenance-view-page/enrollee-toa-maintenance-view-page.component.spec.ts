import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeToaMaintenanceViewPageComponent } from './enrollee-toa-maintenance-view-page.component';

describe('EnrolleeToaMaintenanceViewPageComponent', () => {
  let component: EnrolleeToaMaintenanceViewPageComponent;
  let fixture: ComponentFixture<EnrolleeToaMaintenanceViewPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrolleeToaMaintenanceViewPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeToaMaintenanceViewPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
