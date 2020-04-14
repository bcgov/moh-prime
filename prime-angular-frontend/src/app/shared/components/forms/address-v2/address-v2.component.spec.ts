import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddressV2Component } from './address-v2.component';

describe('AddressV2Component', () => {
  let component: AddressV2Component;
  let fixture: ComponentFixture<AddressV2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddressV2Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddressV2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
