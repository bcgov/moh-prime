import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HoursOperationOverviewComponent } from './hours-operation-overview.component';

describe('HoursOperationOverviewComponent', () => {
  let component: HoursOperationOverviewComponent;
  let fixture: ComponentFixture<HoursOperationOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HoursOperationOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HoursOperationOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
