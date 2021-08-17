import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { EnrolmentComponent } from './enrolment.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { KeycloakService } from 'keycloak-angular';

describe('EnrolmentComponent', () => {
  let component: EnrolmentComponent;
  let fixture: ComponentFixture<EnrolmentComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule,
          NgxBusyModule,
          NgxContextualHelpModule,
          NgxMaterialModule,
          RouterTestingModule,
          AdjudicationModule
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          KeycloakService
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
