import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { HoursOperationComponent } from './hours-operation.component';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('HoursOperationComponent', () => {
  let component: HoursOperationComponent;
  let fixture: ComponentFixture<HoursOperationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        BrowserAnimationsModule,
        HttpClientTestingModule,
        SiteRegistrationModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HoursOperationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
