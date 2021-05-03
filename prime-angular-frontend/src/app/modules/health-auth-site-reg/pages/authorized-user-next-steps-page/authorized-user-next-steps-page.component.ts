import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-authorized-user-next-steps-page',
  templateUrl: './authorized-user-next-steps-page.component.html',
  styleUrls: ['./authorized-user-next-steps-page.component.scss']
})
export class AuthorizedUserNextStepsPageComponent implements OnInit {
  public isAutomatic: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router
  ) { }

  public ngOnInit(): void { }
}
