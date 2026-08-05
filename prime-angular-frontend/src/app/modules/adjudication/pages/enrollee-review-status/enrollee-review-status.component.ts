import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-enrollee-review-status',
  templateUrl: './enrollee-review-status.component.html',
  styleUrls: ['./enrollee-review-status.component.scss'],
  standalone: false
})
export class EnrolleeReviewStatusComponent implements OnInit {
  public hasActions: boolean;

  constructor(
    protected route: ActivatedRoute,
  ) {
    this.hasActions = true;
  }

  public onAction(): void { }

  public ngOnInit(): void { }
}
