import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule, FormGroup } from '@angular/forms';

import { NgxMaskModule } from 'ngx-mask';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { AddressComponent } from './address.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxMaterialModule } from '@shared/modules/ngx-material/ngx-material.module';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';

describe('AddressComponent', () => {
  let component: AddressComponent;
  let fixture: ComponentFixture<AddressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          BrowserAnimationsModule,
          HttpClientTestingModule,
          NgxMaterialModule,
          NgxMaskModule.forRoot(),
          ReactiveFormsModule
        ],
        declarations: [
          AddressComponent
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
          EnrolmentStateService
        ]
      }
    ).compileComponents();
  }));

  beforeEach(inject([EnrolmentStateService], (enrolmentStateService: EnrolmentStateService) => {
    fixture = TestBed.createComponent(AddressComponent);
    component = fixture.componentInstance;
    // Add the bound FormGroup to the component
    component.form = enrolmentStateService.profileForm.get('physicalAddress') as FormGroup;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
