import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { RegulatoryOverviewComponent } from './regulatory-overview.component';

import { DefaultPipe } from '@shared/pipes/default.pipe';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('RegulatoryOverviewComponent', () => {
  let component: RegulatoryOverviewComponent;
  let fixture: ComponentFixture<RegulatoryOverviewComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        RegulatoryOverviewComponent,
        DefaultPipe
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RegulatoryOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
