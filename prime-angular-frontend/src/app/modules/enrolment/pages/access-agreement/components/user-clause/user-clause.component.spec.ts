import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserClauseComponent } from './user-clause.component';

describe('UserClauseComponent', () => {
  let component: UserClauseComponent;
  let fixture: ComponentFixture<UserClauseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UserClauseComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UserClauseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
