import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeMaintenancePageComponent } from './enrollee-maintenance-page.component';

describe('EnrolleeMaintenancePageComponent', () => {
  let component: EnrolleeMaintenancePageComponent;
  let fixture: ComponentFixture<EnrolleeMaintenancePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrolleeMaintenancePageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeMaintenancePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
