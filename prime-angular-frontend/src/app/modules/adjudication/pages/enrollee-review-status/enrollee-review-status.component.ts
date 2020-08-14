import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-enrollee-review-status',
  templateUrl: './enrollee-review-status.component.html',
  styleUrls: ['./enrollee-review-status.component.scss']
})
export class EnrolleeReviewStatusComponent implements OnInit {
  public hasActions: boolean;

  constructor() {
    this.hasActions = true;
  }

  public ngOnInit(): void { }
}
