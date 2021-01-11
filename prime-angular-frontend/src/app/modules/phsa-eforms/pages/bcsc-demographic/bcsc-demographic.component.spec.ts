import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BcscDemographicComponent } from './bcsc-demographic.component';

describe('BcscDemographicComponent', () => {
  let component: BcscDemographicComponent;
  let fixture: ComponentFixture<BcscDemographicComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BcscDemographicComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BcscDemographicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
