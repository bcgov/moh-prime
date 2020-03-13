import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeTableComponent } from './enrollee-table.component';

describe('EnrolleeTableComponent', () => {
  let component: EnrolleeTableComponent;
  let fixture: ComponentFixture<EnrolleeTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolleeTableComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
