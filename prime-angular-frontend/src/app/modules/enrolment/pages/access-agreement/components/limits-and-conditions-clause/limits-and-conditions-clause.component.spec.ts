import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LimitsAndConditionsClauseComponent } from './limits-and-conditions-clause.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';

describe('LimitsAndConditionsClauseComponent', () => {
  let component: LimitsAndConditionsClauseComponent;
  let fixture: ComponentFixture<LimitsAndConditionsClauseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxContextualHelpModule
      ],
      declarations: [
        LimitsAndConditionsClauseComponent,
        PageSubheaderComponent
      ]
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
