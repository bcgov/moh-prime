import { Component, EventEmitter, OnInit, Output, Input } from '@angular/core';
import { BaseDocument } from '../document-upload/document-upload/document-upload.component';

@Component({
  selector: 'app-document-attachment',
  templateUrl: './document-attachment.component.html',
  styleUrls: ['./document-attachment.component.scss']
})
export class DocumentAttachmentComponent implements OnInit {
  @Input() public documents: BaseDocument[];
  @Output() public download: EventEmitter<{ documentId: number }>;

  constructor() {
    this.download = new EventEmitter<{ documentId: number }>();
  }

  public onDownload(documentId: number): void {
    this.download.emit({ documentId });
  }

  ngOnInit(): void {
  }

}
