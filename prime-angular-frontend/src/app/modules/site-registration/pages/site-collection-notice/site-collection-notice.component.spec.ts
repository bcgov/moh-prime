import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { SiteCollectionNoticeComponent } from './site-collection-notice.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SiteRegistrationModule } from '@registration/site-registration.module';

describe('SiteCollectionNoticeComponent', () => {
  let component: SiteCollectionNoticeComponent;
  let fixture: ComponentFixture<SiteCollectionNoticeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SiteRegistrationModule,
        RouterTestingModule
      ],
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
    fixture = TestBed.createComponent(SiteCollectionNoticeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
