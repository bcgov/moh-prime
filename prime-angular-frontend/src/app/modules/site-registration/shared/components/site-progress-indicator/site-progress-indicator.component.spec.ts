import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';

import { MockOrganizationService } from 'test/mocks/mock-organization.service';

import { SharedModule } from '@shared/shared.module';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { SiteProgressIndicatorComponent } from './site-progress-indicator.component';

describe('SiteProgressIndicatorComponent', () => {
  let component: SiteProgressIndicatorComponent;
  let fixture: ComponentFixture<SiteProgressIndicatorComponent>;

  beforeEach( async () => {
    await TestBed.configureTestingModule({
      imports: [
        SharedModule,
        RouterTestingModule
      ],
      declarations: [
        SiteProgressIndicatorComponent
      ],
      providers: [
        {
          provide: OrganizationService,
          useClass: MockOrganizationService
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteProgressIndicatorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
