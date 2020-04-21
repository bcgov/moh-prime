import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-registrant-profile-review',
  templateUrl: './registrant-profile-review.component.html',
  styleUrls: ['./registrant-profile-review.component.scss']
})
export class RegistrantProfileReviewComponent implements OnInit {
  // TODO dropped Registrant model replace with Site
  @Input() registrant: any;

  constructor() { }

  public ngOnInit() { }
}
