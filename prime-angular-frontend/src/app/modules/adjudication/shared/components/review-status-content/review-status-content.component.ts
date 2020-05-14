import { Component, OnInit, Input } from '@angular/core';

import { Reason } from '../../pipes/status-reasons.pipe';
import { Status } from '@adjudication/pages/enrollee-review-status/pipes/statuses.pipe';

@Component({
  selector: 'app-review-status-content',
  templateUrl: './review-status-content.component.html',
  styleUrls: ['./review-status-content.component.scss']
})
export class ReviewStatusContentComponent implements OnInit {
  @Input() public reasons: Reason[];
  @Input() public statuses: Status[];

  constructor() { }

  public ngOnInit() {
  }
}
