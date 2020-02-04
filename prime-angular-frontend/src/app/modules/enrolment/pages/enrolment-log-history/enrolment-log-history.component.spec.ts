import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolmentLogHistoryComponent } from './enrolment-log-history.component';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { EnrolmentModule } from '@enrolment/enrolment.module';

describe('EnrolmentLogHistoryComponent', () => {
  let component: EnrolmentLogHistoryComponent;
  let fixture: ComponentFixture<EnrolmentLogHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxBusyModule,
          EnrolmentModule
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
