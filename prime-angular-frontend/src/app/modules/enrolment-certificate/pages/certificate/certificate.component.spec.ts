import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';

import { CertificateComponent } from './certificate.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { ClipboardIconComponent } from '@shared/components/clipboard-icon/clipboard-icon.component';
import { CertificatePipe } from '@shared/pipes/certificate.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { ClipboardModule } from 'ngx-clipboard';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { RouterTestingModule } from '@angular/router/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CertificateComponent', () => {
  let component: CertificateComponent;
  let fixture: ComponentFixture<CertificateComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxBusyModule,
        NgxContextualHelpModule,
        ClipboardModule,
        NgxMaterialModule,
        RouterTestingModule,
        HttpClientTestingModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      declarations: [
        CertificateComponent,
        PageHeaderComponent,
        PageSubheaderComponent,
        ClipboardIconComponent,
        CertificatePipe,
        FormatDatePipe
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CertificateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
