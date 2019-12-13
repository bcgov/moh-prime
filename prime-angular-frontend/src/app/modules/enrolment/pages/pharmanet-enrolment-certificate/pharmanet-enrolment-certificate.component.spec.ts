import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PharmanetEnrolmentCertificateComponent } from './pharmanet-enrolment-certificate.component';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';

describe('PharmanetEnrolmentCertificateComponent', () => {
  let component: PharmanetEnrolmentCertificateComponent;
  let fixture: ComponentFixture<PharmanetEnrolmentCertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxBusyModule
        ],
        declarations: [
          PharmanetEnrolmentCertificateComponent,
          PageHeaderComponent
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PharmanetEnrolmentCertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
