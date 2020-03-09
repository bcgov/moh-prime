import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HoursOperationComponent } from './hours-operation.component';

describe('HoursOperationComponent', () => {
  let component: HoursOperationComponent;
  let fixture: ComponentFixture<HoursOperationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HoursOperationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HoursOperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
