import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteCollectionNoticeComponent } from './site-collection-notice.component';
import { SiteRegistrationModule } from '../../site-registration.module';
import { RouterTestingModule } from '@angular/router/testing';
import { KeycloakService } from 'keycloak-angular';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

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
