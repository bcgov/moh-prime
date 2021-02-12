import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { KeycloakService } from 'keycloak-angular';

import { EnrolleeTableComponent } from './enrollee-table.component';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableDataSource } from '@angular/material/table';
import { EnrolleeListViewModel } from '@shared/models/enrolment.model';

describe('EnrolleeTableComponent', () => {
  let component: EnrolleeTableComponent;
  let fixture: ComponentFixture<EnrolleeTableComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        AdjudicationModule,
        BrowserAnimationsModule
      ],
      declarations: [],
      providers: [
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeTableComponent);
    component = fixture.componentInstance;
    component.dataSource = new MatTableDataSource<EnrolleeListViewModel>();
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
