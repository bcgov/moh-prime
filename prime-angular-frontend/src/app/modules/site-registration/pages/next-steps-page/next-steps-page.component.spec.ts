import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { MockOrganizationService } from 'test/mocks/mock-organization.service';

import { NextStepsPageComponent } from './next-steps-page.component';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('NextStepsPageComponent', () => {
  let component: NextStepsPageComponent;
  let fixture: ComponentFixture<NextStepsPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      providers: [
        {
          provide: OrganizationService,
          useClass: MockOrganizationService
        }
      ],
      declarations: [NextStepsPageComponent],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NextStepsPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
