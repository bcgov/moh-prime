import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';

import { WeekdayPipe } from '@shared/pipes/weekday.pipe';
import { HoursOperationOverviewComponent } from './hours-operation-overview.component';

describe('HoursOperationOverviewComponent', () => {
  let component: HoursOperationOverviewComponent;
  let fixture: ComponentFixture<HoursOperationOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        HoursOperationOverviewComponent,
        WeekdayPipe
      ],
      providers: [],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HoursOperationOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
