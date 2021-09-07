import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentAttachmentsComponent } from './document-attachments.component';
import { RouterTestingModule } from '@angular/router/testing';

import { DefaultPipe } from '@shared/pipes/default.pipe';

describe('DocumentAttachmentsComponent', () => {
  let component: DocumentAttachmentsComponent;
  let fixture: ComponentFixture<DocumentAttachmentsComponent>;
  const mockActivatedRoute = {
    snapshot: { params: { eid: 1 } }
  };
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
  ]

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        DocumentAttachmentsComponent,
        DefaultPipe
      ],
    })
      .compileComponents();
  });

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
