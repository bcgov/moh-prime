import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { Subscription } from 'rxjs';

@Component({
  selector: 'app-page',
  templateUrl: './page.component.html',
  styleUrls: ['./page.component.scss']
})
export class PageComponent implements OnInit {
  @Input() public busy: Subscription;
  @Input() public mode: 'default' | 'full';
  @Input() public showToolbar: boolean;

  @Output() public back: EventEmitter<void>;

  constructor() {
    this.mode = 'default';
    this.showToolbar = false;
    this.back = new EventEmitter<void>();
  }

  public onBack() {
    this.back.emit();
  }

  public ngOnInit() { }
}
