import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { EnrolleeReviewStatusComponent } from './enrollee-review-status.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';

describe('EnrolleeReviewStatusComponent', () => {
  let component: EnrolleeReviewStatusComponent;
  let fixture: ComponentFixture<EnrolleeReviewStatusComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        AdjudicationModule
      ],
      declarations: [],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        KeycloakService
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeReviewStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
