import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdjudicatorActionsComponent } from './adjudicator-actions.component';

describe('AdjudicatorActionsComponent', () => {
  let component: AdjudicatorActionsComponent;
  let fixture: ComponentFixture<AdjudicatorActionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AdjudicatorActionsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdjudicatorActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
