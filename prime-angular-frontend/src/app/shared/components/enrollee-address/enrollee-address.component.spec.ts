import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { EnrolleeAddressComponent } from './enrollee-address.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';

describe('EnrolleeAddressComponent', () => {
  let component: EnrolleeAddressComponent;
  let fixture: ComponentFixture<EnrolleeAddressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule
        ],
        declarations: [
          EnrolleeAddressComponent,
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
