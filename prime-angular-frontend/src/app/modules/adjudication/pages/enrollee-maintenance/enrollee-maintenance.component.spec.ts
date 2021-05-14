import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeMaintenanceComponent } from './enrollee-maintenance.component';

describe('EnrolleeMaintenanceComponent', () => {
  let component: EnrolleeMaintenanceComponent;
  let fixture: ComponentFixture<EnrolleeMaintenanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EnrolleeMaintenanceComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
