import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegistrationTableComponent } from './site-registration-table.component';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { KeycloakService } from 'keycloak-angular';

describe('SiteRegistrationTableComponent', () => {
  let component: SiteRegistrationTableComponent;
  let fixture: ComponentFixture<SiteRegistrationTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        AdjudicationModule
      ],
      providers: [
        KeycloakService
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
