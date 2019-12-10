import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeclinedComponent } from './declined.component';

describe('DeclinedComponent', () => {
  let component: DeclinedComponent;
  let fixture: ComponentFixture<DeclinedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeclinedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeclinedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
