import { AdjudicationModule } from '@adjudication/adjudication.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';

import { HealthAuthorityTableComponent } from './health-authority-table.component';

describe('HealthAuthorityTableComponent', () => {
  let component: HealthAuthorityTableComponent;
  let fixture: ComponentFixture<HealthAuthorityTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule(
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
    )
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthorityTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
