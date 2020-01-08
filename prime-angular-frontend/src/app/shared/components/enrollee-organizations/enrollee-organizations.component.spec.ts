import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeOrganizationsComponent } from './enrollee-organizations.component';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

describe('EnrolleeOrganizationsComponent', () => {
  let component: EnrolleeOrganizationsComponent;
  let fixture: ComponentFixture<EnrolleeOrganizationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          EnrolleeOrganizationsComponent,
          ConfigCodePipe
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          {
            provide: ConfigService,
            useClass: MockConfigService
          }
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
