import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeAccessTermEnrolmentComponent } from './enrollee-access-term-enrolment.component';
import { APP_DI_CONFIG, APP_CONFIG } from 'app/app-config.module';
import { SharedModule } from '@shared/shared.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

describe('EnrolleeAccessTermEnrolmentComponent', () => {
  let component: EnrolleeAccessTermEnrolmentComponent;
  let fixture: ComponentFixture<EnrolleeAccessTermEnrolmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        AdjudicationModule,
        NgxBusyModule,
        NgxMaterialModule,
        HttpClientTestingModule,
        RouterTestingModule,
        SharedModule
      ],
      declarations: [],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeAccessTermEnrolmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
