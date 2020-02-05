import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeclinedAccessAgreementComponent } from './declined-access-agreement.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { EnrolmentModule } from '@enrolment/enrolment.module';

describe('DeclinedAccessAgreementComponent', () => {
  let component: DeclinedAccessAgreementComponent;
  let fixture: ComponentFixture<DeclinedAccessAgreementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          RouterTestingModule,
          NgxMaterialModule,
          NgxBusyModule,
          EnrolmentModule
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeclinedAccessAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
