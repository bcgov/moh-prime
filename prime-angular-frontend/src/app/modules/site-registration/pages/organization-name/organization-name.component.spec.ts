import { async, ComponentFixture, TestBed, fakeAsync, tick, flush } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { Router } from '@angular/router';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { OrganizationNameComponent } from './organization-name.component';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { SiteRoutes } from '@registration/site-registration.routes';

describe('OrganizationNameComponent', () => {
  let component: OrganizationNameComponent;
  let fixture: ComponentFixture<OrganizationNameComponent>;
  let router: Router;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        RouterTestingModule.withRoutes([
          {
            path: SiteRoutes.ORGANIZATION_NAME,
            component: OrganizationNameComponent
          }
        ]),
        SiteRegistrationModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
    }).compileComponents();
  }));

  beforeEach(fakeAsync(() => {
    router = TestBed.inject(Router);
    fixture = TestBed.createComponent(OrganizationNameComponent);
    component = fixture.componentInstance;
    router.navigateByUrl(`/${SiteRoutes.ORGANIZATION_NAME}`);
    flush();
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
