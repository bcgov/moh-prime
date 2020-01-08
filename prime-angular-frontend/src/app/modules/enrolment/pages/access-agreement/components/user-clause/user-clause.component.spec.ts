import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserClauseComponent } from './user-clause.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';

describe('UserClauseComponent', () => {
  let component: UserClauseComponent;
  let fixture: ComponentFixture<UserClauseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        UserClauseComponent,
        PageSubheaderComponent
      ]
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
