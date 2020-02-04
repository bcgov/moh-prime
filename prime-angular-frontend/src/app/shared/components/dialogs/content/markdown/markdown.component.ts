import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { IDialogContent } from '../../dialog-content.model';

@Component({
  selector: 'app-markdown',
  templateUrl: './markdown.component.html',
  styleUrls: ['./markdown.component.scss']
})
export class MarkdownComponent implements OnInit, IDialogContent {

  @Input()
  public data: string;

  @Output() output = new EventEmitter<{ output: boolean }>();

  constructor() { }

  public onChange($event) {
    this.output.emit($event.checked);
  }

  ngOnInit() {
  }

}
