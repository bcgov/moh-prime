import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleePropertyComponent } from './enrollee-property.component';

describe('EnrolleePropertyComponent', () => {
  let component: EnrolleePropertyComponent;
  let fixture: ComponentFixture<EnrolleePropertyComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolleePropertyComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleePropertyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
