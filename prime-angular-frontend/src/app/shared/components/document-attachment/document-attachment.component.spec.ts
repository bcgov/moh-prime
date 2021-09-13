import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DocumentAttachmentComponent } from './document-attachment.component';

describe('DocumentAttachmentComponent', () => {
  let component: DocumentAttachmentComponent;
  let fixture: ComponentFixture<DocumentAttachmentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DocumentAttachmentComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DocumentAttachmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
