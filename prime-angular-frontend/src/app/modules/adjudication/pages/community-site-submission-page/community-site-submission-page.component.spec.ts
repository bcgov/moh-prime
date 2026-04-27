import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CommunitySiteSubmissionPageComponent } from './community-site-submission-page.component';
import { RouterTestingModule } from '@angular/router/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('CommunitySiteSubmissionPageComponent', () => {
  let component: CommunitySiteSubmissionPageComponent;
  let fixture: ComponentFixture<CommunitySiteSubmissionPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    declarations: [CommunitySiteSubmissionPageComponent],
    imports: [BrowserAnimationsModule,
        RouterTestingModule,
        AdjudicationModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        KeycloakService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
})
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CommunitySiteSubmissionPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
