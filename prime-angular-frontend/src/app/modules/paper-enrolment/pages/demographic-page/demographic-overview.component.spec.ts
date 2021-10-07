import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';

import { DefaultPipe } from '@shared/pipes/default.pipe';
import { ReplacePipe } from '@shared/pipes/replace.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { PhonePipe } from '@shared/pipes/phone.pipe';
import { DemographicOverviewComponent } from './demographic-overview.component';

describe('DemographicOverviewComponent', () => {
  let component: DemographicOverviewComponent;
  let fixture: ComponentFixture<DemographicOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        DemographicOverviewComponent,
        DefaultPipe,
        ReplacePipe,
        FormatDatePipe,
        PhonePipe
      ],
      providers: [],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DemographicOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
