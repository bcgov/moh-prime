import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeAddressComponent } from './enrollee-address.component';

describe('EnrolleeAddressComponent', () => {
  let component: EnrolleeAddressComponent;
  let fixture: ComponentFixture<EnrolleeAddressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolleeAddressComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeAddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
