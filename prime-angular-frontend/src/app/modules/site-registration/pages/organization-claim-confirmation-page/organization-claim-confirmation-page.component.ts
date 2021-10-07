import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-organization-claim-confirmation-page',
  templateUrl: './organization-claim-confirmation-page.component.html',
  styleUrls: ['./organization-claim-confirmation-page.component.scss']
})
export class OrganizationClaimConfirmationPageComponent implements OnInit {
  public title: string;
  public isCompleted: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.title = this.route.snapshot.data.title;
  }

  public ngOnInit(): void {
    this.isCompleted = true;
  }
}
