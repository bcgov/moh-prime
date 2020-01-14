import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeProfileComponent } from './enrollee-profile.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { DefaultPipe } from '@shared/pipes/default.pipe';
import { FormatDatePipe } from '@shared/pipes/format-date.pipe';
import { EnrolleePipe } from '@shared/pipes/enrollee.pipe';
import { ConfigCodePipe } from '@config/config-code.pipe';
import { PostalPipe } from '@shared/pipes/postal.pipe';

describe('EnrolleeProfileComponent', () => {
  let component: EnrolleeProfileComponent;
  let fixture: ComponentFixture<EnrolleeProfileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          EnrolleeProfileComponent,
          ConfigCodePipe,
          DefaultPipe,
          FormatDatePipe,
          EnrolleePipe,
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
    fixture = TestBed.createComponent(EnrolleeProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
