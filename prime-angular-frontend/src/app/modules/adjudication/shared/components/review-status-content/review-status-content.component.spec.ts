import * as faker from 'faker';

import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { ReviewStatusContentComponent } from './review-status-content.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { ConfigService } from 'app/config/config.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';
import { EnrolmentStatusReason } from '@shared/enums/enrolment-status-reason.enum';



fdescribe('ReviewStatusContentComponent', () => {
  const mockDocument = {
    id: 1
  } as BaseDocument;
  const mockAdjudicator = { idir: 'idir', id: faker.random.number() };
  const mockEnrolmentStatuses = [
    {
      id: faker.random.number(),
      enrolmentId: faker.random.number(),
      statusCode: faker.random.number({ min: 1, max: 5 }),
      status: faker.random.number(),
      statusDate: faker.date.past(2).toDateString(),
      enrolmentStatusReasons: EnrolmentStatusReason[faker.random.number(), faker.random.number()],
      adjudicator: mockAdjudicator,
      enrolmentStatusReference: null
    },
    {
      id: faker.random.number(),
      enrolmentId: faker.random.number(),
      statusCode: faker.random.number({ min: 1, max: 5 }),
      status: faker.random.number(),
      statusDate: faker.date.past(2).toDateString(),
      enrolmentStatusReasons: EnrolmentStatusReason[faker.random.number(), faker.random.number()],
      adjudicator: mockAdjudicator,
      enrolmentStatusReference: null
    }
  ];

  let component: ReviewStatusContentComponent;
  let fixture: ComponentFixture<ReviewStatusContentComponent>;
  let isSelfDeclaration;

  let spyOnDownloadSelfDeclarationDocument;
  let spyOnDownloadIdentificationDocument;


  let mockHttpEnrollee;


  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        AdjudicationModule,
        HttpClientTestingModule
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
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReviewStatusContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    spyOnDownloadSelfDeclarationDocument = spyOn<any>(component, 'downloadSelfDeclarationDocument');
    spyOnDownloadIdentificationDocument = spyOn<any>(component, 'downloadIdentificationDocument');
    mockHttpEnrollee = null;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing downloadDocument() ', () => {
    describe('with isSelfDeclaration set to true', () => {
      it('should call downloadSelfDeclarationDocument', () => {
        isSelfDeclaration = true;

        component.downloadDocument(mockDocument, isSelfDeclaration);

        expect(spyOnDownloadSelfDeclarationDocument).toHaveBeenCalledWith(mockDocument.id);
        expect(spyOnDownloadIdentificationDocument).toHaveBeenCalledTimes(0);
      });
    });

    describe('with isSelfDeclaration set to false', () => {
      it('should call downloadIdentificationDocument only', () => {
        isSelfDeclaration = false;

        component.downloadDocument(mockDocument, isSelfDeclaration);

        expect(spyOnDownloadIdentificationDocument).toHaveBeenCalledWith(mockDocument.id);
        expect(spyOnDownloadSelfDeclarationDocument).toHaveBeenCalledTimes(0);
      });
    });
  });

  describe('testing generatePreviousStatuses()', () => {
    describe('with enrollee set to undefined', () => {
      it('should return an empty array', () => {
        // @ts-ignore
        expect(component.generatePreviousStatuses(mockHttpEnrollee)).toEqual([]);
      });
    });

    describe('with a defined enrollee with previousStatuses', () => {
      it('with a defined enrollee and enrolmentStatuses', () => {
        mockHttpEnrollee = new MockEnrolmentService().enrolment;
        mockHttpEnrollee.enrolmentStatuses = mockEnrolmentStatuses
        // @ts-ignore
        expect(component.generatePreviousStatuses(mockHttpEnrollee)).not.toEqual([]);
      });
    });
  });
});
