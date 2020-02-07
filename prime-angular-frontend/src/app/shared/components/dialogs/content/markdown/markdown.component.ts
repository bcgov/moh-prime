import { Component, OnInit, Input } from '@angular/core';
import { IDialogContent } from '../../dialog-content.model';

@Component({
  selector: 'app-markdown',
  templateUrl: './markdown.component.html',
  styleUrls: ['./markdown.component.scss']
})
export class MarkdownComponent implements OnInit, IDialogContent {
  @Input() public data: { message: string };

  constructor() {
    this.data = { message: '' };
  }

  public ngOnInit() { }
}
