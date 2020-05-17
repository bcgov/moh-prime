import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessHoursViewComponent } from './business-hours-view.component';

describe('BusinessHoursViewComponent', () => {
  let component: BusinessHoursViewComponent;
  let fixture: ComponentFixture<BusinessHoursViewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BusinessHoursViewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BusinessHoursViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
