import { ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { UntypedFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { provideHttpClientTesting } from '@angular/common/http/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { EmailFormComponent } from './email-form.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('EmailFormComponent', () => {
  let component: EmailFormComponent;
  let fixture: ComponentFixture<EmailFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    declarations: [
        EmailFormComponent
    ],
    imports: [ReactiveFormsModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: ConfigService,
            useClass: MockConfigService
        },
        FormUtilsService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting(),
    ]
})
      .compileComponents();
  });

  beforeEach(inject([UntypedFormBuilder, FormUtilsService],
    (fb: UntypedFormBuilder, formUtilsService: FormUtilsService) => {
      fixture = TestBed.createComponent(EmailFormComponent);
      component = fixture.componentInstance;
      component.form = fb.group({ email: ['', []] });
      fixture.detectChanges();
    }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
