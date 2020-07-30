import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminOrganizationInformationComponent } from './admin-organization-information.component';
import { SharedModule } from '@shared/shared.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';

describe('AdminOrganizationInformationComponent', () => {
  let component: AdminOrganizationInformationComponent;
  let fixture: ComponentFixture<AdminOrganizationInformationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        AdjudicationModule,
        SharedModule
      ],
      declarations: [],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        KeycloakService
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminOrganizationInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
