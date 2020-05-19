import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteTableComponent } from './site-table.component';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { KeycloakService } from 'keycloak-angular';

describe('SiteTableComponent', () => {
  let component: SiteTableComponent;
  let fixture: ComponentFixture<SiteTableComponent>;

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
    fixture = TestBed.createComponent(SiteTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
