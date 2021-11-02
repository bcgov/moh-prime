import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ActivatedRoute } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { ConfigService } from '@config/config.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { RegulatoryPageComponent } from './regulatory-page.component';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';

fdescribe('RegulatoryPageComponent', () => {
  let component: RegulatoryPageComponent;
  let fixture: ComponentFixture<RegulatoryPageComponent>;
  const mockActivatedRoute = {
    snapshot: { params: { eid: 1 } }
  };
  // let mockEnrollee: HttpEnrollee;
  // let mockComponent;

  // let spyOnRouteRelativeTo;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RegulatoryPageComponent],
      imports: [
        NgxMaterialModule,
        ReactiveFormsModule,
        HttpClientTestingModule,
        RouterTestingModule,
        BrowserAnimationsModule
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
        {
          provide: ActivatedRoute,
          useValue: mockActivatedRoute
        }
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegulatoryPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    // spyOnRouteRelativeTo = spyOn(component.routeUtils, 'routeRelativeTo');
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  // TODO: fix unit tests and uncomment
  // describe('testing onBack()', () => {
  //   describe('with profile completed', () => {
  //     it('should call routeRelativeTo with route PaperEnrolmentRoutes.OVERVIEW ', () => {
  //       component.ngOnInit();
  //       component.enrollee = mockEnrollee;
  //       component.enrollee['profileCompleted'] = true;
  //       component.onBack();
  //       expect(spyOnRouteRelativeTo).toHaveBeenCalledOnceWith(PaperEnrolmentRoutes.OVERVIEW);
  //     });
  //   });

  //   describe('with profile not completed', () => {
  //     it('should call routeRelativeTo with route PaperEnrolmentRoutes.CARE_SETTING ', () => {
  //       component.ngOnInit();
  //       component.enrollee = mockEnrollee;
  //       component.enrollee['profileCompleted'] = false;
  //       component.onBack();
  //       expect(spyOnRouteRelativeTo).toHaveBeenCalledOnceWith(PaperEnrolmentRoutes.CARE_SETTING);
  //     });
  //   });
  // });
});
