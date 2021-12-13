import { Component, OnInit, Input } from '@angular/core';

import { Observable } from 'rxjs';

export interface DateContent {
  date: string;
  name?: string;
  content: string;
  marginRight?: string;
}

@Component({
  selector: 'app-dated-content-table',
  templateUrl: './dated-content-table.component.html',
  styleUrls: ['./dated-content-table.component.scss']
})
export class DatedContentTableComponent implements OnInit {
  @Input() public showTime: boolean;
  @Input() public items$: Observable<DateContent[]>;

  constructor() {
    this.showTime = true;
  }

  public ngOnInit() { }
}
