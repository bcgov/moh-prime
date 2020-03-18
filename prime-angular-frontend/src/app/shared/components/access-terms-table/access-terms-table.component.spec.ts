import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KeycloakService } from 'keycloak-angular';

import { AccessTermsTableComponent } from './access-terms-table.component';
import { AdjudicationModule } from '@adjudication/adjudication.module';

describe('AccessTermsTableComponent', () => {
  let component: AccessTermsTableComponent;
  let fixture: ComponentFixture<AccessTermsTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        AdjudicationModule
      ],
      declarations: [],
      providers: [
        KeycloakService
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessTermsTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
