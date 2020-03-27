import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { ConfigService } from '@config/config.service';
import { SharedModule } from '@shared/shared.module';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { EnrolleeOrganizationsComponent } from './enrollee-organizations.component';

describe('EnrolleeOrganizationsComponent', () => {
  let component: EnrolleeOrganizationsComponent;
  let fixture: ComponentFixture<EnrolleeOrganizationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          SharedModule,
          EnrolmentModule
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: ConfigService,
            useClass: MockConfigService
          },
          ConfigCodePipe
        ]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeOrganizationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
