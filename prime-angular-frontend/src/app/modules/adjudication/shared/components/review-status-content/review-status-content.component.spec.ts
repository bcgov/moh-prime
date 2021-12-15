//@ts-nocheck
import * as faker from 'faker';

import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { ReviewStatusContentComponent, Status, Reason } from './review-status-content.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { ConfigService } from 'app/config/config.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';
import { EnrolmentStatusReason } from '@shared/enums/enrolment-status-reason.enum';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';

describe('ReviewStatusContentComponent', () => {
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
  const mockReason = {
    name: 'reason',
    note: 'reason: 1, 2',
    documents: [],
    isSelfDeclaration: false,
    question: "question",
    potentialMatchIds: [],
  };

  const mockSelfDeclarations =
    [
      {
        selfDeclarationTypeCode: 0,
        selfDeclarationDetails: faker.random.words(),
        documentGuids: [faker.random.word()],
        answered: true,
      },
      {
        selfDeclarationTypeCode: 1,
        selfDeclarationDetails: faker.random.words(),
        documentGuids: [faker.random.word()],
        answered: false,
      },
      {
        selfDeclarationTypeCode: 2,
        selfDeclarationDetails: faker.random.words(),
        documentGuids: [faker.random.word()],
        answered: false,
      },
      {
        selfDeclarationTypeCode: 3,
        selfDeclarationDetails: faker.random.words(),
        documentGuids: [faker.random.word()],
        answered: false,
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
    mockHttpEnrollee = new MockEnrolmentService().enrolment;
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
    describe('with enrollee set to null', () => {
      it('should return an empty array', () => {
        mockHttpEnrollee = null;
        expect(component.generatePreviousStatuses(mockHttpEnrollee)).toEqual([]);
      }
      );
    });

    describe('with a defined enrollee with previousStatuses', () => {
      it('should return an array of lenght equal to that of enrolmentStatuses and of type Status[]', () => {
        mockHttpEnrollee.enrolmentStatuses = mockEnrolmentStatuses
        const statuses = component.generatePreviousStatuses(mockHttpEnrollee)

        expect(statuses.length).toEqual(mockHttpEnrollee.enrolmentStatuses.length);
        statuses.forEach((status) => {
          expect(status).toEqual(jasmine.any(Status));
        });
      });
    });
  });

  describe('testing generateReasons', () => {
    describe('with enrollee statusCode other than UNDER_REVIEW', () => {
      it('should return an empty array', () => {
        mockHttpEnrollee.currentStatus.statusCode = EnrolmentStatusEnum.EDITABLE;

        expect(component.generateReasons(mockHttpEnrollee)).toEqual([]);
      });
    });

    describe('with enrollee statusCode set to UNDER_REVIEW', () => {
      it('should return an array of Reason', () => {
        mockHttpEnrollee.currentStatus.statusCode = EnrolmentStatusEnum.UNDER_REVIEW;
        // set statusReasonCode to anything other than SELF_DECLARATION, IDENTITY_PROVIDER, or POSSIBLE_PAPER_ENROLMENT_MATCH
        mockHttpEnrollee.currentStatus.enrolmentStatusReasons[0].statusReasonCode = EnrolmentStatusReason.MANUAL;
        const reasons = component.generateReasons(mockHttpEnrollee);

        reasons.forEach((reason) => {
          expect(reason).toEqual(jasmine.any(Reason));
        });
      });
    });
  });

  describe('testing parseReasons', () => {
    describe('with currentStatus set to null', () => {
      it('should return an empty array', () => {
        mockHttpEnrollee.currentStatus = null;
        expect(component.parseReasons(mockHttpEnrollee.currentStatus)).toEqual([]);
      });
    });

    describe('with statusReasaonCode set to SELF_DECLARATION', () => {
      it('should call parseSelfDeclarations', () => {
        mockHttpEnrollee.currentStatus.enrolmentStatusReasons[0].statusReasonCode = EnrolmentStatusReason.SELF_DECLARATION;
        const spyOnParseSelfDeclarations = spyOn<any>(component, 'parseSelfDeclarations').and.returnValue([]);

        component.parseReasons(mockHttpEnrollee.currentStatus);

        expect(spyOnParseSelfDeclarations).toHaveBeenCalledTimes(1);
      });
    });

    describe('with statusReasaonCode set to POSSIBLE_PAPER_ENROLMENT_MATCH', () => {
      it('should call parsePotentialMatchIds', () => {
        mockHttpEnrollee.currentStatus.enrolmentStatusReasons[0].statusReasonCode = EnrolmentStatusReason.POSSIBLE_PAPER_ENROLMENT_MATCH;
        const spyOnParsePotentialMatchIds = spyOn<any>(component, 'parsePotentialMatchIds');

        component.parseReasons(mockHttpEnrollee.currentStatus);

        expect(spyOnParsePotentialMatchIds).toHaveBeenCalledTimes(1);
      });
    });
  });

  describe('testing parseSelfDeclarations', () => {
    describe('with selfDeclarations', () => {
      it('should return an array of reasons of same length as the selfDeclarations', () => {

        mockHttpEnrollee.selfDeclarations = mockSelfDeclarations;
        const reasons = component.parseSelfDeclarations(mockHttpEnrollee);

        expect(reasons.length).toEqual(1);
      });
    });

    describe('with no selfDeclarations', () => {
      it('should return an empty array', () => {
        mockHttpEnrollee.selfDeclarations = [];
        const reasons = component.parseSelfDeclarations(mockHttpEnrollee);

        expect(reasons).toEqual([]);
      });
    });
  });

  describe('testing parsePotentialMatchIds', () => {
    describe('with selfDeclarations', () => {
      it('short shorten the reasons.notes string and add elements to potentialMatchIds array', () => {
        const noteLength = mockReason.note.length
        const reason = component.parsePotentialMatchIds(mockReason);

        expect(reason.note.length).toBeLessThan(noteLength);
        expect(reason.potentialMatchIds.length).toEqual(2);
      });
    });
  });
});
