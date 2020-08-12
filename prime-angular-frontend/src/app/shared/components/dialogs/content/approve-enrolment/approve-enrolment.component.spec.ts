import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ApproveEnrolmentComponent } from './approve-enrolment.component';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

describe('ApproveEnrolmentComponent', () => {
  let component: ApproveEnrolmentComponent;
  let fixture: ComponentFixture<ApproveEnrolmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxMaterialModule
      ],
      declarations: [ApproveEnrolmentComponent]
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
