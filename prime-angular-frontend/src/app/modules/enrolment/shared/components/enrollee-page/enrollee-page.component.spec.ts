import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleePageComponent } from './enrollee-page.component';

describe('EnrolleePageComponent', () => {
  let component: EnrolleePageComponent;
  let fixture: ComponentFixture<EnrolleePageComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolleePageComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
