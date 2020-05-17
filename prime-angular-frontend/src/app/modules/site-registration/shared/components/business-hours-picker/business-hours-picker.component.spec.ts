import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessHoursPickerComponent } from './business-hours-picker.component';

describe('BusinessHoursPickerComponent', () => {
  let component: BusinessHoursPickerComponent;
  let fixture: ComponentFixture<BusinessHoursPickerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [BusinessHoursPickerComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BusinessHoursPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
