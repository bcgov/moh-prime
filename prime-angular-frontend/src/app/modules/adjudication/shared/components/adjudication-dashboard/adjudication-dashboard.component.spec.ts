import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdjudicationDashboardComponent } from './adjudication-dashboard.component';

describe('AdjudicationDashboardComponent', () => {
  let component: AdjudicationDashboardComponent;
  let fixture: ComponentFixture<AdjudicationDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdjudicationDashboardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdjudicationDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
