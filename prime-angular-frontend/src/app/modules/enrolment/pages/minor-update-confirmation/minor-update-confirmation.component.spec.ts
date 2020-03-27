import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { MinorUpdateConfirmationComponent } from './minor-update-confirmation.component';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { KeycloakService } from 'keycloak-angular';
import { EnrolmentModule } from '@enrolment/enrolment.module';

describe('MinorUpdateConfirmationComponent', () => {
  let component: MinorUpdateConfirmationComponent;
  let fixture: ComponentFixture<MinorUpdateConfirmationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule,
          RouterTestingModule,
          NgxBusyModule,
          NgxContextualHelpModule,
          NgxMaterialModule,
          EnrolmentModule
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
          KeycloakService
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MinorUpdateConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
