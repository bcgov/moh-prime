import { Component, OnInit, Input } from '@angular/core';
import { Enrolment } from '@shared/models/enrolment.model';
import { Reason } from '../../pipes/status-reasons.pipe';

@Component({
  selector: 'app-review-status-content',
  templateUrl: './review-status-content.component.html',
  styleUrls: ['./review-status-content.component.scss']
})
export class ReviewStatusContentComponent implements OnInit {
  @Input() public reasons: Reason[];
  constructor() { }

  ngOnInit() {
  }

}
