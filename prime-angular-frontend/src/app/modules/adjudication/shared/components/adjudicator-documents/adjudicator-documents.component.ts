import { Component, Input, OnInit, Output, EventEmitter, ViewChild } from '@angular/core';
import { AdjudicationDocument } from '@registration/shared/models/adjudication-document.model';
import { DocumentUploadComponent } from '@shared/components/document-upload/document-upload/document-upload.component';
import { BehaviorSubject, Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { DateContent } from '../dated-content-table/dated-content-table.component';

@Component({
  selector: 'app-adjudicator-documents',
  templateUrl: './adjudicator-documents.component.html',
  styleUrls: ['./adjudicator-documents.component.scss']
})
export class AdjudicatorDocumentsComponent implements OnInit {
  @Input() public documents$: Observable<AdjudicationDocument[]>;
  @Output() public saveDocuments: EventEmitter<string[]>;
  @Output() public getDocumentByGuid: EventEmitter<number>;
  @ViewChild('documentUpload') public documentUploadComponent: DocumentUploadComponent;

  public documentGuids: string[];

  public uploadedFile: boolean;

  constructor(
  ) {
    this.saveDocuments = new EventEmitter<string[]>();
    this.getDocumentByGuid = new EventEmitter<number>();
    this.documentGuids = [];
  }

  public onSubmit() {
    if (this.documentGuids?.length) {
      this.saveDocuments.emit(this.documentGuids);
    }
  }

  public removeFiles() {
    this.documentUploadComponent.removeFiles();
  }

  public onUpload(document: AdjudicationDocument) {
    this.documentGuids.push(document.documentGuid);
  }

  public onRemoveDocument(documentGuid: string) {
    delete this.documentGuids[documentGuid];
  }

  public getDocument(documentId: number) {
    this.getDocumentByGuid.emit(documentId);
  }

  ngOnInit(): void {
  }

}
