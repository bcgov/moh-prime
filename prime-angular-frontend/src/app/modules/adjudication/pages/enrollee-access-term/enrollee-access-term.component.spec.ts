import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeAccessTermComponent } from './enrollee-access-term.component';
import { SharedModule } from '@shared/shared.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('EnrolleeAccessTermComponent', () => {
  let component: EnrolleeAccessTermComponent;
  let fixture: ComponentFixture<EnrolleeAccessTermComponent>;

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
    fixture = TestBed.createComponent(EnrolleeAccessTermComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
