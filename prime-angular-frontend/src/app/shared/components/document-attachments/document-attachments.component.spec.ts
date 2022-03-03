import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { DocumentAttachmentsComponent } from './document-attachments.component';
import { RouterTestingModule } from '@angular/router/testing';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';

import { DefaultPipe } from '@shared/pipes/default.pipe';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('DocumentAttachmentsComponent', () => {
  let component: DocumentAttachmentsComponent;
  let fixture: ComponentFixture<DocumentAttachmentsComponent>;
  const mockDocuments = [
    {
      id: 1,
      filename: 'test1.pdf',
      documentGuid: '111-111-111',
      documentType: 1
    },
    {
      id: 2,
      filename: 'test2.pdf',
      documentGuid: '222-222-222',
      documentType: 2
    },
    {
      id: 3,
      filename: 'test3.pdf',
      documentGuid: '333-333-333',
      documentType: 3
    },
  ];

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        MatSnackBarModule
      ],
      declarations: [
        DocumentAttachmentsComponent,
        DefaultPipe
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: AppConfig
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentAttachmentsComponent);
    component = fixture.componentInstance;
    component.documents = mockDocuments;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
