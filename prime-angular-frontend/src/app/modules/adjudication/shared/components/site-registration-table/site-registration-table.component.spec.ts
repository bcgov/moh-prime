import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegistrationTableComponent } from './site-registration-table.component';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { KeycloakService } from 'keycloak-angular';
import { ActivatedRoute } from '@angular/router';

describe('SiteRegistrationTableComponent', () => {
  let component: SiteRegistrationTableComponent;
  let fixture: ComponentFixture<SiteRegistrationTableComponent>;
  const mockActivatedRoute = {
    snapshot: {
      params: {
        sid: 1
      }
    }
  };

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        AdjudicationModule
      ],
      providers: [
        KeycloakService,
        {
          provide: ActivatedRoute,
          useValue: mockActivatedRoute
        }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
