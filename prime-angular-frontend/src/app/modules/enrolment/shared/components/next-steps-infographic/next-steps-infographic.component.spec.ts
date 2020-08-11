import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KeycloakService } from 'keycloak-angular';

import { NextStepsInfographicComponent } from './next-steps-infographic.component';
import { SharedModule } from '@shared/shared.module';
import { EnrolmentModule } from '@enrolment/enrolment.module';

describe('NextStepsInfographicComponent', () => {
  let component: NextStepsInfographicComponent;
  let fixture: ComponentFixture<NextStepsInfographicComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SharedModule,
        EnrolmentModule
      ],
      providers: [
        KeycloakService
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NextStepsInfographicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
