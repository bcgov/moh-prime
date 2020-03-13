import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolmentsComponent } from './enrolments.component';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { SharedModule } from '@shared/shared.module';

describe('EnrolmentsComponent', () => {
  let component: EnrolmentsComponent;
  let fixture: ComponentFixture<EnrolmentsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          SharedModule,
          AdjudicationModule
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolmentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
