import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthCareTypeOverviewComponent } from './health-auth-care-type-overview.component';

describe('HealthAuthCareTypeOverviewComponent', () => {
  let component: HealthAuthCareTypeOverviewComponent;
  let fixture: ComponentFixture<HealthAuthCareTypeOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HealthAuthCareTypeOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthCareTypeOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
