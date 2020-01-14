import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeOrganizationsComponent } from './enrollee-organizations.component';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { EnrolleePropertyComponent } from '@shared/components/enrollee/enrollee-property/enrollee-property.component';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';

describe('EnrolleeOrganizationsComponent', () => {
  let component: EnrolleeOrganizationsComponent;
  let fixture: ComponentFixture<EnrolleeOrganizationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          NgxContextualHelpModule
        ],
        declarations: [
          EnrolleeOrganizationsComponent,
          PageSubheaderComponent,
          EnrolleePropertyComponent,
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
