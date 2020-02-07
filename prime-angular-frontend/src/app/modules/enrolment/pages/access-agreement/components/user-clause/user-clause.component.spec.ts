import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserClauseComponent } from './user-clause.component';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { EnrolmentModule } from '@enrolment/enrolment.module';

describe('UserClauseComponent', () => {
  let component: UserClauseComponent;
  let fixture: ComponentFixture<UserClauseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxContextualHelpModule,
        EnrolmentModule
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
