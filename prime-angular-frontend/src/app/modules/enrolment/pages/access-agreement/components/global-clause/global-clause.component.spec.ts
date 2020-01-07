import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalClauseComponent } from './global-clause.component';

describe('GlobalClauseComponent', () => {
  let component: GlobalClauseComponent;
  let fixture: ComponentFixture<GlobalClauseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GlobalClauseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GlobalClauseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
