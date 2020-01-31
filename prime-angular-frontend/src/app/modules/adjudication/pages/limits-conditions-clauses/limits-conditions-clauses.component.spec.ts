import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { LimitsConditionsClausesComponent } from './limits-conditions-clauses.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { PageComponent } from '@shared/components/page/page.component';
import { EditorModule } from '@tinymce/tinymce-angular';

describe('LimitsConditionsClausesComponent', () => {
  let component: LimitsConditionsClausesComponent;
  let fixture: ComponentFixture<LimitsConditionsClausesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        NgxBusyModule,
        NgxContextualHelpModule,
        NgxMaterialModule,
        ReactiveFormsModule,
        RouterTestingModule,
        EditorModule
      ],
      declarations: [
        LimitsConditionsClausesComponent,
        PageComponent,
        PageHeaderComponent,
        FormatDatePipe
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ConfigService,
          useValue: MockConfigService
        }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LimitsConditionsClausesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
