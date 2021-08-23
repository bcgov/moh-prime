import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';

import { DefaultPipe } from '@shared/pipes/default.pipe';
import { HealthAuthCareTypeOverviewComponent } from './health-auth-care-type-overview.component';

describe('HealthAuthCareTypeOverviewComponent', () => {
  let component: HealthAuthCareTypeOverviewComponent;
  let fixture: ComponentFixture<HealthAuthCareTypeOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        HealthAuthCareTypeOverviewComponent,
        DefaultPipe
      ],
      providers: [

      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthCareTypeOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
