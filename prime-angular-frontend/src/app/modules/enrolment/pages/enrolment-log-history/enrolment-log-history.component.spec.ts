import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolmentLogHistoryComponent } from './enrolment-log-history.component';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';

describe('EnrolmentLogHistoryComponent', () => {
  let component: EnrolmentLogHistoryComponent;
  let fixture: ComponentFixture<EnrolmentLogHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxBusyModule
        ],
        declarations: [
          EnrolmentLogHistoryComponent,
          PageComponent,
          PageHeaderComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolmentLogHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
