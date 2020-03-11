import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeEventsComponent } from './enrollee-events.component';

describe('EnrolleeEventsComponent', () => {
  let component: EnrolleeEventsComponent;
  let fixture: ComponentFixture<EnrolleeEventsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolleeEventsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
