import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LimitsAndConditionsClauseComponent } from './limits-and-conditions-clause.component';

describe('LimitsAndConditionsClauseComponent', () => {
  let component: LimitsAndConditionsClauseComponent;
  let fixture: ComponentFixture<LimitsAndConditionsClauseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LimitsAndConditionsClauseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LimitsAndConditionsClauseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
