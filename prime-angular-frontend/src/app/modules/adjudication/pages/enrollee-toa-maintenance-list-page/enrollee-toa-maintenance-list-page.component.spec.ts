import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeToaMaintenanceListPageComponent } from './enrollee-toa-maintenance-list-page.component';

describe('EnrolleeToaMaintenanceListPageComponent', () => {
  let component: EnrolleeToaMaintenanceListPageComponent;
  let fixture: ComponentFixture<EnrolleeToaMaintenanceListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrolleeToaMaintenanceListPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeToaMaintenanceListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
