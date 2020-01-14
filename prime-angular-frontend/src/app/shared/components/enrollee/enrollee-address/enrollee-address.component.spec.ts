import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { EnrolleeAddressComponent } from './enrollee-address.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';
import { PageSubheaderComponent } from '@shared/components/page-subheader/page-subheader.component';
import { EnrolleePropertyComponent } from '@shared/components/enrollee/enrollee-property/enrollee-property.component';
import { NgxContextualHelpModule } from '@shared/modules/ngx-contextual-help/ngx-contextual-help.module';

describe('EnrolleeAddressComponent', () => {
  let component: EnrolleeAddressComponent;
  let fixture: ComponentFixture<EnrolleeAddressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule,
          NgxContextualHelpModule
        ],
        declarations: [
          EnrolleeAddressComponent,
          PageSubheaderComponent,
          EnrolleePropertyComponent,
          DefaultPipe,
          ConfigCodePipe,
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
    fixture = TestBed.createComponent(EnrolleeAddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
