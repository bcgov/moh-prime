import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { EnrolleeProfileComponent } from './enrollee-profile.component';
import { EnrolleePropertyErrorComponent } from '../enrollee-property-error/enrollee-property-error.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { EnrolleePipe } from '@shared/pipes/enrollee.pipe';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';

describe('EnrolleeProfileComponent', () => {
  let component: EnrolleeProfileComponent;
  let fixture: ComponentFixture<EnrolleeProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule,
          NgxMaterialModule
        ],
        declarations: [
          EnrolleeProfileComponent,
          EnrolleePropertyErrorComponent,
          ConfigCodePipe,
          DefaultPipe,
          FormatDatePipe,
          EnrolleePipe,
          PostalPipe
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
    fixture = TestBed.createComponent(EnrolleeProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
