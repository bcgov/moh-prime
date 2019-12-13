import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessAgreementHistoryComponent } from './access-agreement-history.component';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';

describe('AccessAgreementHistoryComponent', () => {
  let component: AccessAgreementHistoryComponent;
  let fixture: ComponentFixture<AccessAgreementHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxBusyModule
        ],
        declarations: [
          AccessAgreementHistoryComponent,
          PageHeaderComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessAgreementHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
