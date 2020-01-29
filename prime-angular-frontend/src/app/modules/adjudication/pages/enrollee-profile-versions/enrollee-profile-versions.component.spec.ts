import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { EnrolleeProfileVersionsComponent } from './enrollee-profile-versions.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { PageComponent } from '@shared/components/page/page.component';
import { PageHeaderComponent } from '@shared/components/page-header/page-header.component';
import { NgxBusyModule } from '@shared/modules/ngx-busy/ngx-busy.module';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';

describe('EnrolleeProfileVersionsComponent', () => {
  let component: EnrolleeProfileVersionsComponent;
  let fixture: ComponentFixture<EnrolleeProfileVersionsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxBusyModule,
          NgxMaterialModule,
          RouterTestingModule,
          HttpClientTestingModule
        ],
        declarations: [
          EnrolleeProfileVersionsComponent,
          PageComponent,
          PageHeaderComponent,
          FormatDatePipe
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeProfileVersionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
