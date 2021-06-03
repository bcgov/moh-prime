import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeToaMaintenancePageComponent } from './enrollee-toa-maintenance-page.component';

describe('EnrolleeToaMaintenancePageComponent', () => {
  let component: EnrolleeToaMaintenancePageComponent;
  let fixture: ComponentFixture<EnrolleeToaMaintenancePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrolleeToaMaintenancePageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeToaMaintenancePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
