import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-submission-confirmation-page',
  templateUrl: './submission-confirmation-page.component.html',
  styleUrls: ['./submission-confirmation-page.component.scss']
})
export class SubmissionConfirmationPageComponent implements OnInit {
  public title: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.title = route.snapshot.data.title;
  }

  public ngOnInit(): void { }
}
