import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GlobalClauseComponent } from './global-clause.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';

describe('GlobalClauseComponent', () => {
  let component: GlobalClauseComponent;
  let fixture: ComponentFixture<GlobalClauseComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxContextualHelpModule
      ],
      declarations: [
        GlobalClauseComponent,
        PageSubheaderComponent
      ]
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
