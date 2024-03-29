import * as faker from 'faker';

import { NEVER, Observable, of } from 'rxjs';
import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { EnrolleeAdjudicatorDocumentsComponent } from './enrollee-adjudicator-documents.component';
import { AdjudicatorDocumentsComponent } from '@adjudication/shared/components/adjudicator-documents/adjudicator-documents.component';
import { Component, CUSTOM_ELEMENTS_SCHEMA, EventEmitter, Input, Output } from '@angular/core';
import { AdjudicationDocument } from '@registration/shared/models/adjudication-document.model';

describe('EnrolleeAdjudicatorDocumentsComponent', () => {
  let component: EnrolleeAdjudicatorDocumentsComponent;
  let fixture: ComponentFixture<EnrolleeAdjudicatorDocumentsComponent>;

  let spyOnCreateEnrolleeAdjudicationDocument;
  let spyOnGetEnrolleeAdjudicationDocumentDownloadToken;
  let spyOnGetDocuments;

  let mockEnrolleeId;
  let mockAdjudicatorId;
  let mockGuidArray;
  let mockDocumentId;

  @Component({
    selector: 'app-adjudicator-document',
    template: '',
    providers: [
      {
        provide: AdjudicatorDocumentsComponent,
        useClass: AdjudicatorDocumentsStubComponent
      }]
  })
  class AdjudicatorDocumentsStubComponent {
    @Input() public documents$: Observable<AdjudicationDocument[]>;
    @Output() public saveDocuments: EventEmitter<string[]>;
    @Output() public getDocumentByGuid: EventEmitter<number>;
    @Output() public deleteDocumentById: EventEmitter<number>;
    public documentGuids: string[];
    public uploadedFile: boolean;

    removeFiles = jasmine.createSpy('removeFiles');
    onSubmit = () => { };
    onUpload = () => { };
    onRemoveDocument = () => { };
    getDocument = () => { };
    onDelete = () => { };
    ngOnInit = () => { };
    documentUploadComponent = null;
  }

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [
        EnrolleeAdjudicatorDocumentsComponent,
        AdjudicatorDocumentsStubComponent
      ],
      imports: [
        RouterTestingModule,
        ReactiveFormsModule,
        HttpClientTestingModule,
        MatSnackBarModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeAdjudicatorDocumentsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    component.adjudicatorDocumentsComponent = new AdjudicatorDocumentsStubComponent();
  });

  it('should create', () => {
    expect(component.adjudicatorDocumentsComponent).toBeTruthy();
    expect(component).toBeTruthy();
  });

  describe('testing onSaveDocuments()', () => {
    beforeEach(() => {
      mockEnrolleeId = faker.random.number();
      mockAdjudicatorId = faker.random.number();
      (component as any).route.snapshot.params.id = mockEnrolleeId;
      spyOnGetDocuments = spyOn((component as any), 'getDocuments');
      spyOnCreateEnrolleeAdjudicationDocument = spyOn(
        (component as any).enrolmentResource,
        'createEnrolleeAdjudicationDocument')
        .and.returnValue(of({ enrolleeId: mockEnrolleeId, adjudicatorId: mockAdjudicatorId }));
    });

    describe(('with one document guid'), () => {
      it('should call getDocuments() once', () => {
        mockGuidArray = [faker.random.uuid()];
        component.onSaveDocuments(mockGuidArray);

        expect(spyOnGetDocuments).toHaveBeenCalledWith(mockEnrolleeId);
        expect(spyOnCreateEnrolleeAdjudicationDocument).toHaveBeenCalledTimes(mockGuidArray.length);
      });
    });

    describe('with multiple document guids', () => {
      it('should call getDocuments() as many times as there are documents', () => {
        const guidArray = Array.from(
          { length: faker.random.number({ min: 2, max: 10 }) },
          () => faker.random.uuid()
        );
        component.onSaveDocuments(guidArray);

        expect(spyOnGetDocuments).toHaveBeenCalledWith(mockEnrolleeId);
        expect(spyOnCreateEnrolleeAdjudicationDocument).toHaveBeenCalledTimes(guidArray.length);
      });
    });

    describe('with no document guid', () => {
      it(('should not call getDocuments'), () => {
        const guidArray = [];
        component.onSaveDocuments(guidArray);

        expect(spyOnCreateEnrolleeAdjudicationDocument).not.toHaveBeenCalled();
        expect(spyOnGetDocuments).not.toHaveBeenCalled();
      });
    });
  });

  describe('testing onGetDocumentByGuid', () => {
    let spyOnDownloadToken;
    let mockToken;

    beforeEach(() => {
      mockToken = faker.random.word()
      spyOnGetEnrolleeAdjudicationDocumentDownloadToken = spyOn(
        (component as any).enrolmentResource,
        'getEnrolleeAdjudicationDocumentDownloadToken')
        .and.returnValue(of(mockToken));

      spyOnDownloadToken = spyOn((component as any).utilsService, 'downloadToken');
    });

    describe('with a getEnrolleeAdjudicationDocumentDownloadToken completing properly', () => {
      it('should call downloadToken', () => {
        component.onGetDocumentByGuid(1);
        expect(spyOnDownloadToken).toHaveBeenCalledWith(mockToken);
      });
    });

    describe('with a getEnrolleeAdjudicationDocumentDownloadToken not completing', () => {
      it('should not call downloadToken', () => {
        spyOnGetEnrolleeAdjudicationDocumentDownloadToken.and.returnValue(NEVER);
        component.onGetDocumentByGuid(1);
        expect(spyOnDownloadToken).not.toHaveBeenCalled();
      });
    });
  });

  describe('testing onDeleteDocumentById', () => {
    let spyOnDeleteEnrolleeAdjudicationDocument;

    beforeEach(() => {
      mockEnrolleeId = faker.random.number();
      mockAdjudicatorId = faker.random.number();
      (component as any).route.snapshot.params.id = mockEnrolleeId;
      spyOnGetDocuments = spyOn((component as any), 'getDocuments');
      spyOnDeleteEnrolleeAdjudicationDocument = spyOn(
        (component as any).enrolmentResource,
        'deleteEnrolleeAdjudicationDocument')
        .and.returnValue(of({ enrolleeId: mockEnrolleeId, adjudicatorId: mockAdjudicatorId }));
    });

    describe('with deleteEnrolleeAdjudicationDocument properly called', () => {
      it('should call getDocuments()', () => {
        mockDocumentId = faker.random.number();

        component.onDeleteDocumentById(mockDocumentId);
        expect(spyOnGetDocuments).toHaveBeenCalledTimes(1);
      });
    });

    describe('with deleteEnrolleeAdjudicationDocument throwing error', () => {
      it('should not call getDocuments()', () => {
        // We can use try/catch block or override the spy method to return an Observable<Never> instead
        spyOnDeleteEnrolleeAdjudicationDocument.and.returnValue(NEVER);
        mockDocumentId = faker.random.number();
        component.onDeleteDocumentById(mockDocumentId);
        expect(spyOnGetDocuments).not.toHaveBeenCalled();
      });
    });
  });
});
