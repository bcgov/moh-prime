import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { RemoteAccessComponent } from './remote-access.component';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';


describe('RemoteAccessComponent', () => {
  let component: RemoteAccessComponent;
  let fixture: ComponentFixture<RemoteAccessComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        NgxMaterialModule,
        HttpClientTestingModule,
        ReactiveFormsModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: EnrolmentService,
          useClass: MockEnrolmentService
        }
      ],
      declarations: [RemoteAccessComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoteAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
