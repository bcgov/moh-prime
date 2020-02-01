import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessTermsComponent } from './access-terms.component';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { RouterTestingModule } from '@angular/router/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

describe('AccessTermsComponent', () => {
  let component: AccessTermsComponent;
  let fixture: ComponentFixture<AccessTermsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxBusyModule,
          NgxMaterialModule,
          RouterTestingModule,
          HttpClientTestingModule,
          RouterTestingModule
        ],
        declarations: [
          AccessTermsComponent,
          PageComponent,
          PageHeaderComponent,
          FormatDatePipe
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: EnrolmentService,
            useClass: MockEnrolmentService
          },
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessTermsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
