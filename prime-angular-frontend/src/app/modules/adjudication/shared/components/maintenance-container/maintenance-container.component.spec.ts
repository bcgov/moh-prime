import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MaintenanceContainerComponent } from './maintenance-container.component';

describe('MaintenanceContainerComponent', () => {
  let component: MaintenanceContainerComponent;
  let fixture: ComponentFixture<MaintenanceContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MaintenanceContainerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MaintenanceContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
