import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhsaLabtechDashboardComponent } from './phsa-labtech-dashboard.component';

describe('PhsaLabtechDashboardComponent', () => {
  let component: PhsaLabtechDashboardComponent;
  let fixture: ComponentFixture<PhsaLabtechDashboardComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [PhsaLabtechDashboardComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhsaLabtechDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
