import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeToaMaintenanceComponent } from './enrollee-toa-maintenance.component';

describe('EnrolleeToaMaintenanceComponent', () => {
  let component: EnrolleeToaMaintenanceComponent;
  let fixture: ComponentFixture<EnrolleeToaMaintenanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrolleeToaMaintenanceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeToaMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
