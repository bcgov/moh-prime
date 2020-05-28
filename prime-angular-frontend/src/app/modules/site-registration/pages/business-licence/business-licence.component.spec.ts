import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BusinessLicenceComponent } from './business-licence.component';

describe('BusinessLicenceComponent', () => {
  let component: BusinessLicenceComponent;
  let fixture: ComponentFixture<BusinessLicenceComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BusinessLicenceComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BusinessLicenceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
