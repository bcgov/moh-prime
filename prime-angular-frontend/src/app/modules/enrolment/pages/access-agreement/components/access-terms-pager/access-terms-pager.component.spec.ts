import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessTermsPagerComponent } from './access-terms-pager.component';
import { GlobalClauseComponent } from '../global-clause/global-clause.component';
import { UserClauseComponent } from '../user-clause/user-clause.component';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { LicenceClassClauseComponent } from '../licence-class-clause/licence-class-clause.component';
import { LimitsAndConditionsClauseComponent } from '../limits-and-conditions-clause/limits-and-conditions-clause.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { ContextualHelpComponent } from '@shared/modules/ngx-contextual-help/contextual-help/contextual-help.component';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

describe('TermsOfAccessPagerComponent', () => {
  let component: AccessTermsPagerComponent;
  let fixture: ComponentFixture<AccessTermsPagerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxMaterialModule
        ],
        declarations: [
          AccessTermsPagerComponent,
          PageSubheaderComponent,
          GlobalClauseComponent,
          UserClauseComponent,
          LicenceClassClauseComponent,
          LimitsAndConditionsClauseComponent,
          ContextualHelpComponent,
          ConfigCodePipe
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessTermsPagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
