import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';
import { EnrolleePrivilegesComponent } from './enrollee-privileges.component';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { EnrolleePropertyComponent } from '../enrollee-property/enrollee-property.component';
import { MatExpansionModule } from '@angular/material';
import { APP_DI_CONFIG, APP_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { PrimeEmailComponent } from '@shared/components/prime-email/prime-email.component';
import { PrimePhoneComponent } from '@shared/components/prime-phone/prime-phone.component';


describe('EnrolleePrivilegesComponent', () => {
  let component: EnrolleePrivilegesComponent;
  let fixture: ComponentFixture<EnrolleePrivilegesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxContextualHelpModule,
        MatExpansionModule,
        HttpClientTestingModule
      ],
      declarations: [
        EnrolleePrivilegesComponent,
        PageSubheaderComponent,
        DefaultPipe,
        EnrolleePropertyComponent,
        PrimeEmailComponent,
        PrimePhoneComponent
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

      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleePrivilegesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
