import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RuAccessAgreementComponent } from './ru-access-agreement.component';
import { PageRefDirective } from '../../page-ref.directive';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';

describe('RuAccessAgreementComponent', () => {
  let component: RuAccessAgreementComponent;
  let fixture: ComponentFixture<RuAccessAgreementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxContextualHelpModule,
        ],
        declarations: [
          PageSubheaderComponent,
          PageRefDirective,
          RuAccessAgreementComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RuAccessAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
