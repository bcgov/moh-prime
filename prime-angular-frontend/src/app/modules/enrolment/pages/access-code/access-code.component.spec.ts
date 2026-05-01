import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

import { AccessCodeComponent } from './access-code.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('AccessCodeComponent', () => {
  let component: AccessCodeComponent;
  let fixture: ComponentFixture<AccessCodeComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [AccessCodeComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [BrowserAnimationsModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: AuthService,
            useClass: MockAuthService
        },
        {
            provide: ConfigService,
            useClass: MockConfigService
        },
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessCodeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
