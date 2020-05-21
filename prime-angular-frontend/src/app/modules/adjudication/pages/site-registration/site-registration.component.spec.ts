import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegistrationComponent } from './site-registration.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('SiteRegistrationComponent', () => {
  let component: SiteRegistrationComponent;
  let fixture: ComponentFixture<SiteRegistrationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        NgxBusyModule,
        NgxContextualHelpModule,
        NgxMaterialModule,
        RouterTestingModule,
        AdjudicationModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
