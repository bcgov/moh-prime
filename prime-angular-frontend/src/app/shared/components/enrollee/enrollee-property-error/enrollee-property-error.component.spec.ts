import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleePropertyErrorComponent } from './enrollee-property-error.component';

describe('EnrolleePropertyErrorComponent', () => {
  let component: EnrolleePropertyErrorComponent;
  let fixture: ComponentFixture<EnrolleePropertyErrorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolleePropertyErrorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleePropertyErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
