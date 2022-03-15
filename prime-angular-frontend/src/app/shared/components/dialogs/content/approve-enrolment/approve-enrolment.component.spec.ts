import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveEnrolmentComponent } from './approve-enrolment.component';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('ApproveEnrolmentComponent', () => {
  let component: ApproveEnrolmentComponent;
  let fixture: ComponentFixture<ApproveEnrolmentComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxMaterialModule
      ],
      declarations: [ApproveEnrolmentComponent],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ApproveEnrolmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
