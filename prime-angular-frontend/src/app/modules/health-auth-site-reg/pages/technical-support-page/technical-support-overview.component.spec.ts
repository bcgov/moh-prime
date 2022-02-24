import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { TechnicalSupportOverviewComponent } from './technical-support-overview.component';
import { FullnamePipe } from '@shared/pipes/fullname.pipe';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('TechnicalSupportOverviewComponent', () => {
  let component: TechnicalSupportOverviewComponent;
  let fixture: ComponentFixture<TechnicalSupportOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        TechnicalSupportOverviewComponent,
        FullnamePipe,
        DefaultPipe
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TechnicalSupportOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
