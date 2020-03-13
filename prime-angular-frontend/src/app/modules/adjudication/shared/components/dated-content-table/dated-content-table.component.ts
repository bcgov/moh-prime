import { Component, OnInit, Input } from '@angular/core';

import { Observable } from 'rxjs';

export interface DateContent {
  date: string;
  content: string;
}

@Component({
  selector: 'app-dated-content-table',
  templateUrl: './dated-content-table.component.html',
  styleUrls: ['./dated-content-table.component.scss']
})
export class DatedContentTableComponent implements OnInit {
  @Input() public items$: Observable<DateContent[]>;
  @Input() public hasTime: boolean;

  constructor() {
    this.hasTime = true;
  }

  public ngOnInit() { }
}
