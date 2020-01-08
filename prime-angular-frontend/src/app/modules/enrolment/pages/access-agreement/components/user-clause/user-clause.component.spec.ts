import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UserClauseComponent } from './user-clause.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';

describe('UserClauseComponent', () => {
  let component: UserClauseComponent;
  let fixture: ComponentFixture<UserClauseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxContextualHelpModule
      ],
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
