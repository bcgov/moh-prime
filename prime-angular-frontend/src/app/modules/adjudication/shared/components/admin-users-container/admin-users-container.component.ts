import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-admin-users-container',
  templateUrl: './admin-users-container.component.html',
  styleUrls: ['./admin-users-container.component.scss']
})
export class AdminUsersContainerComponent implements OnInit {
  public busy: Subscription;

  constructor(
  ) {
  }

  ngOnInit(): void {
  }
}
