import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { IDialogContent } from '../../dialog-content.model';

@Component({
  selector: 'app-markdown',
  templateUrl: './markdown.component.html',
  styleUrls: ['./markdown.component.scss']
})
export class MarkdownComponent implements OnInit, IDialogContent {

  @Input()
  public data: { message: string };

  constructor() { }

  ngOnInit() {
  }

}
